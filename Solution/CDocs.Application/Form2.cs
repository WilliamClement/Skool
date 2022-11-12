using CDocs.Core;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace CDocs.Application
{
    public partial class Form2 : Form
    {
        public Form2(string path)
        {
            InitializeComponent();

            Vault = new(new Uri(path));
            Vault.ErrorsChanged += Vault_ErrorsChanged;

            // Считаем, сколько нарушений имеется и сколько устранено
            int points = 0;
            int max_points = 0;

            // Добавляем данные в таблицу
            List<KeyValuePair<string, string>> violations = new();
            foreach (var handler in Vault)
            {
                if (typeof(FixesReport).IsInstanceOfType(handler.Report))
                {
                    var report = handler.Report as FixesReport;
                    foreach (Fix fix in report.Fixes)
                    {
                        dataGridView1.Rows.Add(new string[]
                        {
                            report.Number,
                            report.ViolationsNumber,
                            fix.Number,
                            fix.Responsible,
                            fix.Notified.ToString(),
                            fix.Done.ToString()
                        });

                        if (fix.Done)
                        {
                            // Увеличиваем счетчик количества устранений из соответствующих
                            // отчетов с флагом об устранении
                            points++;
                        }

                        violations.Add(new KeyValuePair<string, string>(report.ViolationsNumber, fix.Number));
                    }
                }
                else if (typeof(ViolationsReport).IsInstanceOfType(handler.Report))
                {
                    var report = handler.Report as ViolationsReport;

                    // Увеличиваем счетчик количества нарушений из соответствующих отчетов
                    max_points += report.Violations.Count;
                }
            }

            // Добавляем в таблицу нарушения, по которым нет отчетов FixesReport или Fix
            foreach (var handler in Vault)
            {
                if (typeof(ViolationsReport).IsInstanceOfType(handler.Report))
                {
                    var report = handler.Report as ViolationsReport;

                    foreach (var violation in report.Violations)
                    {
                        bool skip = false;
                        foreach (var declared in violations)
                        {
                            if (declared.Key == report.Number && declared.Value == violation.Number)
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (skip) continue;

                        dataGridView1.Rows.Add(new string[]
                        {
                            "",
                            report.Number,
                            violation.Number,
                            "",
                            "False",
                            "False"
                        });
                    }
                }
            }

            // Обновляем значение ProgressBar прогрессом устранения всех выявленных нарушений
            double progress = points / (double)max_points;
            progressBar1.Value = (int)(100 * progress);
        }

        private DocumentVault Vault;

        private void Vault_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            foreach (string error in Vault.GetErrors())
            {
                Debugger.Break();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

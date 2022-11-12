namespace CDocs.Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                var stream = new StreamReader("pathes.dat");
                var line = stream.ReadLine();
                while (line != null)
                {
                    listView1.Items.Add(line);
                    line = stream.ReadLine();
                }
                stream.Close();
            }
            catch
            {
                listView1.Items.Add("C:/");
                listView1.Items.Add("D:/");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var path = folderBrowserDialog1.SelectedPath;

                var stream = new StreamWriter("pathes.dat", true);
                stream.WriteLine(path);
                stream.Close();

                listView1.Items.Add(path);

                var form = new Form2(path);
                Hide();
                form.Closed += (s, e) => Show();
                form.Show();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }

            var path = listView1.SelectedItems[0].Text;
            var form = new Form2(path);
            form.Closed += (s, e) => Show();
            Hide();
            form.Show();
        }
    }
}
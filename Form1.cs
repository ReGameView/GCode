using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        //Оптимизация: -1
        //Качество кода == Оптимизации
        //Время: сдать вчера
        //Стоимость: Бесплатно
       
        public Form1()
        {
            InitializeComponent();
        }

        public StreamReader file;
        public int count = 0;
        public string nameFile = "";

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "GCode|*.gcode";
            openFileDialog1.Title = "Select a GCode File";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dataGridView1.Visible = true;
                clearTable();
                button1.Visible = true;
                nameFile = openFileDialog1.SafeFileName;
                file = new StreamReader(openFileDialog1.FileName);
                Main();
            }
        }

        private void создатьGCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearTable();
            button1.Visible = true;
            dataGridView1.Visible = true;
        }

        private void Main()
        {
            DataGridViewCell main = new DataGridViewCheckBoxCell(); 
            DataGridViewCell X = new DataGridViewCheckBoxCell(); 
            DataGridViewCell Y = new DataGridViewCheckBoxCell(); 
            DataGridViewCell Z = new DataGridViewCheckBoxCell();
            DataGridViewCell E = new DataGridViewCheckBoxCell(); 
            DataGridViewCell P = new DataGridViewCheckBoxCell(); 
            DataGridViewCell S = new DataGridViewCheckBoxCell(); 
            DataGridViewCell F = new DataGridViewCheckBoxCell(); 
            DataGridViewCell last = new DataGridViewCheckBoxCell();
           // DataGridViewRow row = new DataGridViewRow();
            

            //Добавить комментарии установки температуры, и размеры координат
            count = 0;
            string line;
            short rows = 0;
            short N = 0;
            int len = 0;
            while(true)
            {
                last.Value = "";
                F.Value = "";
                S.Value = "";
                P.Value = "";
                E.Value = "";
                Z.Value = "";
                Y.Value = "";
                X.Value = "";
                main.Value = "";

                line = file.ReadLine();
                if(line == null)
                {
                    break;
                }
                len = line.Length;

                for (int i = 0; i != len; i++)
                {
                    switch (line[i])
                    {
                        case 'G': last = main; main.Value +="G" + line[i + 1].ToString(); i++; break;
                        case 'M': last = main; main.Value +="M" + line[i + 1].ToString(); i++; break;
                        case 'X': last = X; X.Value += line[i + 1].ToString(); i++; break;
                        case 'Y': last = Y; Y.Value += line[i + 1].ToString(); i++; break;
                        case 'Z': last = Z; Z.Value += line[i + 1].ToString(); i++; break;
                        case 'E': last = E; E.Value += line[i + 1].ToString(); i++; break;
                        case 'P': last = P; P.Value += line[i + 1].ToString(); i++; break;
                        case 'S': last = S; S.Value += line[i + 1].ToString(); i++; break;
                        case 'F': last = F; F.Value += line[i + 1].ToString(); i++; break;
                        case '0': last.Value += line[i].ToString(); break;
                        case '1': last.Value += line[i].ToString(); break;
                        case '2': last.Value += line[i].ToString(); break;
                        case '3': last.Value += line[i].ToString(); break;
                        case '4': last.Value += line[i].ToString(); break;
                        case '5': last.Value += line[i].ToString(); break;
                        case '6': last.Value += line[i].ToString(); break;
                        case '7': last.Value += line[i].ToString(); break;
                        case '8': last.Value += line[i].ToString(); break;
                        case '9': last.Value += line[i].ToString(); break;
                        case ' ':
                            
                            break;
                        case '.': last.Value += line[i].ToString(); break;
                    }
                    
                    //dataGridView1.Rows[count].Cells[rows].Value += n.ToString();
                } //end for
                string[] row = { main.Value.ToString(), X.Value.ToString(), Y.Value.ToString(), Z.Value.ToString(), E.Value.ToString(), P.Value.ToString(), S.Value.ToString(), F.Value.ToString() };
                dataGridView1.Rows.Add(row);
                // dataGridView1.Rows.Insert(count);
                count++;
                
            } // end while
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            date();
            string filename = nameFile;
            System.IO.File.WriteAllText(filename, label1.Text);
            MessageBox.Show("Файл сохранен");
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            date();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "GCode|*.gcode|All files(*.*)|*.*";
            saveFileDialog1.Title = "Save an GCode file";
            saveFileDialog1.FileName = nameFile;
            saveFileDialog1.DefaultExt = ".gcode";
            saveFileDialog1.ShowDialog();
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, label1.Text);
            MessageBox.Show("Файл сохранен");
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void clearTable()
        {
            dataGridView1.Rows.Clear();
            //for (int i = 0; i != )
        }

        private void date()
        {
            label1.Text = "";
            string[] results = new string[dataGridView1.RowCount];
            for(int i = 0; i != dataGridView1.RowCount; i++)
            {
                results[i] = dataGridView1.Rows[i].Cells[0].Value.ToString(); //G
                for(int j = 1; j != dataGridView1.ColumnCount; j++)
                {
                    string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    if (value != "")
                    {
                        results[i] += " ";
                        results[i] += dataGridView1.Columns[j].Name.ToString();
                        results[i] += value;
                    }
                    
                }
                results[i] += "\n";
                label1.Text += results[i];
            }
        }
    }
}

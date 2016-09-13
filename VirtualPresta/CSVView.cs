using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.IO;

namespace VirtualPresta
{
    public partial class CSVView : UserControl
    {
        [EditorAttribute(
            "System.ComponentModel.Design.MultilineStringEditor, System.Design",
            "System.Drawing.Design.UITypeEditor")]
        public string CSV {
            get
            {
                StringWriter stringWriter = new StringWriter();
                CsvWriter writer = new CsvWriter(stringWriter, new CsvHelper.Configuration.CsvConfiguration() { Delimiter = ";"});
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    writer.WriteField(column.HeaderText);
                }
                writer.NextRecord();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        writer.WriteField(cell.Value.ToString());
                    }
                    writer.NextRecord();

                }
                return stringWriter.ToString();
            }
            set
            {
                StringReader stringReader = new StringReader(value);
                CsvReader reader = new CsvReader(stringReader, new CsvHelper.Configuration.CsvConfiguration() { Delimiter = ";" });

                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();
                try {
                    reader.ReadHeader();
                }
                catch (Exception)
                {
                    return;
                }
                foreach (string column in reader.FieldHeaders)
                {
                    dataGridView.Columns.Add(new DataGridViewColumn() { HeaderText = column, CellTemplate = new DataGridViewTextBoxCell() });
                }
                while (reader.Read())
                {
                    dataGridView.Rows.Add();
                    int counter = 0;
                    foreach (string column in reader.FieldHeaders)
                    {
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[counter].Value = reader.GetField(counter);
                        dataGridView.Rows[dataGridView.Rows.Count - 1].Height = dataGridView.Height - dataGridView.ColumnHeadersHeight;
                        counter++;
                    }
                }
            }
        }
        

        public CSVView()
        {
            InitializeComponent();
        }
    }
}

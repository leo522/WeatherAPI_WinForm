using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace Weather_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        /// <summary>
        /// ���o��e����ɶ�
        /// </summary>
        private void UpdateDateTime()
        {
            lbl_DateTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// �B�z���쪺�Ѯ�API���
        /// </summary>
        private void GetWeatherData()
        {
            try
            {
                string uri = "https://opendata.cwa.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWB-FB153F5B-564E-4E95-8593-938F29D5B004";
                JArray jsondata = getJson(uri);

                listView_Weather.View = View.Details;
                listView_Weather.GridLines = true;
                listView_Weather.LabelEdit = false;
                listView_Weather.FullRowSelect = true;
                listView_Weather.Columns.Add("�a��", 100);
                listView_Weather.Columns.Add("�Ѯ�", 100);
                listView_Weather.Columns.Add("�ū�", 100);
                listView_Weather.Columns.Add("���B���v", 100);

                foreach (JObject data in jsondata)
                {
                    string loactionname = (string)data["locationName"]; //�a�W
                    string weathdescrible = (string)data["weatherElement"][0]["time"][0]["parameter"]["parameterName"]; //�Ѯ𪬪p
                    string pop = (string)data["weatherElement"][1]["time"][0]["parameter"]["parameterName"];  //���B���v
                    string mintemperature = (string)data["weatherElement"][2]["time"][0]["parameter"]["parameterName"]; //�̧C�ū�
                    string maxtemperature = (string)data["weatherElement"][4]["time"][0]["parameter"]["parameterName"]; //�̰��ū�

                    ListViewItem dto = new ListViewItem(loactionname);
                    dto.SubItems.Add(weathdescrible); // �Ѯ�
                    dto.SubItems.Add($"{mintemperature}�Xc-{maxtemperature}�Xc"); // �ū�
                    dto.SubItems.Add($"{pop}%"); // ���B���v

                    listView_Weather.Items.Add(dto); // �K�[ ListViewItem �� ListView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// �q��H�p���oAPI���
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        static public JArray getJson(string uri)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Timeout = 10000;
                req.Method = "GET";
                HttpWebResponse respone = (HttpWebResponse)req.GetResponse();
                StreamReader streamReader = new StreamReader(respone.GetResponseStream(), Encoding.UTF8);
                string result = streamReader.ReadToEnd();

                respone.Close();
                streamReader.Close();

                JObject jsondata = JsonConvert.DeserializeObject<JObject>(result);

                return (JArray)jsondata["records"]["location"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ���sĲ�o���oAPI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getData_btn_Click(object sender, EventArgs e)
        {
            listView_Weather.Items.Clear();
            GetWeatherData();
        }
    }
}
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

                foreach (JObject data in jsondata)
                {
                    string loactionname = (string)data["locationName"]; //�a�W
                    string weathdescrible = (string)data["weatherElement"][0]["time"][0]["parameter"]["parameterName"]; //�Ѯ𪬪p
                    string pop = (string)data["weatherElement"][1]["time"][0]["parameter"]["parameterName"];  //���B���v
                    string mintemperature = (string)data["weatherElement"][2]["time"][0]["parameter"]["parameterName"]; //�̧C�ū�
                    string maxtemperature = (string)data["weatherElement"][4]["time"][0]["parameter"]["parameterName"]; //�̰��ū�

                    // �b�o�̱N�����ܦb���W�����󤤡A�Ҧp�G
                    listBoxWeatherInfo.Items.Add($"{loactionname} �Ѯ�:{weathdescrible} �ū�:{mintemperature}�Xc-{maxtemperature}�Xc ���B���v:{pop}%");
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
            listBoxWeatherInfo.Items.Clear();
            GetWeatherData();
        }
    }
}
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
        /// 取得當前日期時間
        /// </summary>
        private void UpdateDateTime()
        {
            lbl_DateTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 處理取到的天氣API資料
        /// </summary>
        private void GetWeatherData()
        {
            try
            {
                string uri = "https://opendata.cwa.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization=CWA-95FC4BA4-1CD9-46CC-838C-570CEE5379F5";
                JArray jsondata = getJson(uri);

                listView_Weather.View = View.Details;
                listView_Weather.GridLines = true;
                listView_Weather.LabelEdit = false;
                listView_Weather.FullRowSelect = true;
                listView_Weather.Columns.Add("地區", 100);
                listView_Weather.Columns.Add("天氣", 100);
                listView_Weather.Columns.Add("溫度", 100);
                listView_Weather.Columns.Add("降雨機率", 100);

                foreach (JObject data in jsondata)
                {
                    string loactionname = (string)data["locationName"]; //地名
                    string weathdescrible = (string)data["weatherElement"][0]["time"][0]["parameter"]["parameterName"]; //天氣狀況
                    string pop = (string)data["weatherElement"][1]["time"][0]["parameter"]["parameterName"];  //降雨機率
                    string mintemperature = (string)data["weatherElement"][2]["time"][0]["parameter"]["parameterName"]; //最低溫度
                    string maxtemperature = (string)data["weatherElement"][4]["time"][0]["parameter"]["parameterName"]; //最高溫度

                    ListViewItem dto = new ListViewItem(loactionname);
                    dto.SubItems.Add(weathdescrible); // 天氣
                    dto.SubItems.Add($"{mintemperature}°c-{maxtemperature}°c"); // 溫度
                    dto.SubItems.Add($"{pop}%"); // 降雨機率

                    listView_Weather.Items.Add(dto); // 添加 ListViewItem 到 ListView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 從氣象署取得API資料
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
        /// 按鈕觸發取得API
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

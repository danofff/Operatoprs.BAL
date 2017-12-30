using Operatoprs.BAL.Operator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Operatoprs.BAL.Administrator
{
    public class Administrator
    {

        private string pathTosave { get; set; } = "";

        public Administrator():this("")
        { }
        public Administrator (string path)
        {
           pathTosave = path;
        }
        public bool createOperator(IOperator op, out string message)
        {
            if (!string.IsNullOrEmpty(pathTosave))
            {
                XmlDocument doc = getOperator();
                XmlElement root = doc.DocumentElement;
                //че то там заменили, че то получилось итд.
                //XmlElement root = doc.CreateElement("Operators");

                XmlElement prefixes = doc.CreateElement("prefixes");

                foreach (var pr in op.pref)
                {
                    XmlElement pref = doc.CreateElement("prefix"+pr);
                    pref.InnerText = pr.ToString();
                    prefixes.AppendChild(pref);
                }
              
                XmlElement logo = doc.CreateElement("logo");
                logo.InnerText = op.logo;

                XmlElement name = doc.CreateElement("operatorName");
                name.InnerText = op.nameOperator;

                XmlElement percent = doc.CreateElement("percent");
                percent.InnerText = op.percent.ToString();

                root.AppendChild(prefixes);
                root.AppendChild(logo);
                root.AppendChild(name);
                root.AppendChild(percent);

                doc.AppendChild(root);
                try
                {
                    doc.Save(string.Format("{0}/operators.xml", pathTosave));
                    message = "";
                    return true;
                }
                catch(Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
            }
            else
            {
                message = "path for save is not define";
                return false;
            }
        }

        public XmlDocument getOperator()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Format("{0}/operators.xml", pathTosave));
            return doc;
        }

        public bool CreateOperatorSerialize(IOperator op,out string message)
        {
            #region найти файл хранящий операторов
            IOperator[] oplist = null;
            XmlSerializer formatter = new XmlSerializer(typeof(IOperator[]));
            using (FileStream fs = new FileStream(string.Format("{0}/operators.xml",
                pathTosave), FileMode.OpenOrCreate))
            {
                oplist = (IOperator[])formatter.Deserialize(fs);
            }
            #endregion

            #region добавить в массив передаваемого оператора
            List<IOperator> nOpList = oplist.ToList();
            nOpList.Add(op);
            oplist = nOpList.ToArray();
            #endregion

            #region сериализируем новый массив
            using (FileStream fs=new FileStream(string.Format("{0}/operators.xml",
                pathTosave), FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, oplist);
            }
            #endregion
            message = "good";
            return true;
        }
    }
}

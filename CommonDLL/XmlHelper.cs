using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonDLL
{
/// <summary>
    /// XML帮助类
    /// </summary>
    public class XmlHelper
    {
        #region 属性
        /// <summary>
        /// XML文件路径
        /// </summary>
        public static string XmlFilePath { get; set; }
        #endregion

        /// <summary>
        /// 根据节点属性名称获取属性值
        /// </summary>
        /// <param name="rootNodeName">根节点名称</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="inPropertyName">传入属性名称</param>
        /// <param name="inPropertyValue">传入属性值</param>
        /// <param name="outPropertyName">返回属性名称</param>
        /// <returns>返回属性名称值</returns>
        public static string GetProValueByProName(string rootNodeName, string nodeName, string inPropertyName, string inPropertyValue, string outPropertyName)
        {
            string result = "";
            try
            {
                //判断文件是否存在,不存则创建
                if (!File.Exists(XmlFilePath))
                {
                    XDocument xmlDoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "no"),
                                        new XElement(rootNodeName
                                         )
                                     );
                    xmlDoc.Save(XmlFilePath);
                }
                //加载模板XML
                XElement rootNode = XElement.Load(XmlFilePath);
                //查询语句
                IEnumerable<XElement> targetNodes = from target in rootNode.Descendants(nodeName) where target.Attribute(inPropertyName).Value.Equals(inPropertyValue) select target
    ;
                //遍历所获得的目标节点（集合）
                foreach (XElement node in targetNodes)
                {
                    result = node.Attribute(outPropertyName).Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 根据节点属性名称节点值
        /// </summary>
        /// <param name="rootNodeName">根节点名称</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns>返回节点值</returns>
        public static string GetProValueByValue(string rootNodeName, string nodeName, string propertyName, string propertyValue)
        {
            string result = "";
            try
            {
                //判断文件是否存在,不存则创建
                if (!File.Exists(XmlFilePath))
                {
                    XDocument xmlDoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "no"),
                                        new XElement(rootNodeName
                                         )
                                     );
                    xmlDoc.Save(XmlFilePath);
                }
                //加载模板XML
                XElement rootNode = XElement.Load(XmlFilePath);
                //查询语句
                IEnumerable<XElement> targetNodes = from target in rootNode.Descendants(nodeName) where target.Attribute(propertyName).Value.Equals(propertyValue) select target
    ;
                //遍历所获得的目标节点（集合）
                foreach (XElement node in targetNodes)
                {
                    result = node.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 根据节点属性名称设置节点值
        /// </summary>
        /// <param name="rootNodeName">根节点名称</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <param name="value">值</param>
        /// <returns>返回是否成功</returns>
        public static bool SetProValueByValue(string rootNodeName, string nodeName, string propertyName, string propertyValue, string value)
        {
            bool result = false;
            try
            {
                //判断文件是否存在,不存则创建
                if (!File.Exists(XmlFilePath))
                {
                    XDocument xmlDoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "no"),
                                        new XElement(rootNodeName
                                         )
                                     );
                    xmlDoc.Save(XmlFilePath);
                }
                //加载模板XML
                XElement rootNode = XElement.Load(XmlFilePath);
                //查询语句
                IEnumerable<XElement> targetNodes = from target in rootNode.Descendants(nodeName) where target.Attribute(propertyName).Value.Equals(propertyValue) select target
    ;
                //遍历所获得的目标节点（集合）
                foreach (XElement node in targetNodes)
                {
                    node.Value = value;
                }
                rootNode.Save(XmlFilePath);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 根据节点属性名称设置属性值
        /// </summary>
        /// <param name="rootNodeName">根节点名称</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="inPropertyName">传入属性名称</param>
        /// <param name="inPropertyValue">传入属性值</param>
        /// <param name="propertyName">要设置属性名称</param>
        /// <param name="propertyValue">要设置属性值</param>
        /// <returns>返回是否成功</returns>
        public static bool SetProValueByProValue(string rootNodeName, string nodeName, string inPropertyName, string inPropertyValue, string propertyName, string propertyValue)
        {
            bool result = false;
            try
            {
                //判断文件是否存在,不存则创建
                if (!File.Exists(XmlFilePath))
                {
                    XDocument xmlDoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "no"),
                                        new XElement(rootNodeName
                                         )
                                     );
                    xmlDoc.Save(XmlFilePath);
                }
                //加载模板XML
                XElement rootNode = XElement.Load(XmlFilePath);
                //查询语句
                IEnumerable<XElement> targetNodes = from target in rootNode.Descendants(nodeName) where target.Attribute(inPropertyName).Value.Equals(inPropertyValue) select target
    ;
                //遍历所获得的目标节点（集合）
                foreach (XElement node in targetNodes)
                {
                    node.SetAttributeValue(propertyName, propertyValue);
                }
                rootNode.Save(XmlFilePath);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}

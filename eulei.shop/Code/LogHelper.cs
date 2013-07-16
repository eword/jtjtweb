using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace eulei.shop.Code
{
    public class LogHelper
    {
        private const string FILE_NAME = "ErrorLog.log";
        private const string OPERATE_FILE_NAME = "OperateLog.log";
        private static string BASE_PATH = System.AppDomain.CurrentDomain.BaseDirectory;
        private const string FIRST_PATH = "log";
        private const string SECOND_PATH = "Error";
        private const string SECOND_OPERATE_PATH = "Operate";
        private static string DetectDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static void WriteErrorLog(string str)
        {
            string path = BASE_PATH + FIRST_PATH;
            path = DetectDirectory(path);
            path += "\\" + SECOND_PATH;
            path = DetectDirectory(path);
            Directory.SetCurrentDirectory(path);
            StreamWriter sr;    
            int _fileOrder = 0;
            string filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + FILE_NAME;                     
            while(File.Exists(filePath))
            {
                _fileOrder++;
                filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + FILE_NAME;
            };
            _fileOrder=_fileOrder>0?_fileOrder-1:0;
            filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + FILE_NAME;            
            if (File.Exists(filePath)) //如果文件存在,则创建File.AppendText对象 
            {
                FileInfo _fileinfo = new FileInfo(filePath);
                if (!(_fileinfo.Length > 1024000))
                {
                    sr = File.AppendText(filePath);
                }
                else
                {
                    _fileOrder++;
                    filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + FILE_NAME;
                    sr = File.CreateText(filePath);

                }
            }
            else    //如果文件不存在,则创建File.CreateText对象 
            {
                sr = File.CreateText(filePath);
            }

            sr.WriteLine(DateTime.Now.ToString() + ":=>" + str);
            sr.Flush();
            sr.Close();

        }
        public static void WriteSendLog(string str)
        {
            string path = BASE_PATH + FIRST_PATH;
            path = DetectDirectory(path);
            path += "\\" + SECOND_OPERATE_PATH;
            path = DetectDirectory(path);
            Directory.SetCurrentDirectory(path);
            StreamWriter sr;
            int _fileOrder = 0;
            string filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + OPERATE_FILE_NAME;
            while (File.Exists(filePath))
            {
                _fileOrder++;
                filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + OPERATE_FILE_NAME;
            };
            _fileOrder = _fileOrder > 0 ? _fileOrder-1 : 0;
            filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + OPERATE_FILE_NAME; 
            if (File.Exists(filePath)) //如果文件存在,则创建File.AppendText对象 
            {
                FileInfo _fileinfo = new FileInfo(filePath);
                if (!(_fileinfo.Length > 1024000))
                {
                    sr = File.AppendText(filePath);
                }
                else
                {
                    _fileOrder++;
                    filePath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToString("yyyyMMdd") + _fileOrder.ToString() + OPERATE_FILE_NAME;
                    sr = File.CreateText(filePath);
                }
            }
            else    //如果文件不存在,则创建File.CreateText对象 
            {
                sr = File.CreateText(filePath);
            }
            sr.WriteLine(DateTime.Now.ToString()+":=>"+ str);
            sr.Flush();
            sr.Close();
        }
    }
}
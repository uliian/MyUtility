using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyUtility.Tools
{
    public class ProjectFileInfo
    {
        public ProjectFileInfo(string fullPath)
        {
            this.FullPath = fullPath;
            this.ProjectFileName = Path.GetFileName(fullPath);
            this.ProjectName = Path.GetFileNameWithoutExtension(fullPath);
            this.ProjectPath = Path.GetDirectoryName(fullPath);
        }

        public string FullPath { get; set; }

        public string ProjectFileName { get; set; }

        public string ProjectName { get; set; }

        public string ProjectPath { get; set; }

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return $"{nameof(this.ProjectName)}: {this.ProjectName}";
        }
    }
}

namespace ArchiveSiteReBuilder.Lib
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        public Constants.Settings.RebuildMode RebuildMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Constants.Settings.OverwriteMode OverwriteMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AlwaysCheckFilesAfterParsing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Save(string filePath)
        {
            try
            {
                var nameSpace = new XmlSerializerNamespaces();
                nameSpace.Add("", "");
                var serializer = new XmlSerializer(typeof(Settings));
                using (TextWriter writer = new StreamWriter(filePath))
                    serializer.Serialize(writer, this, nameSpace);

                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Load(string filePath)
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(Settings));
                var settings = this;
                try
                {
                    using (TextReader reader = new StreamReader(filePath))
                        settings = (Settings)deserializer.Deserialize(reader);
                }
                catch (Exception) { return false; }

                this.RebuildMode = settings.RebuildMode;
                this.OverwriteMode = settings.OverwriteMode;
                this.AlwaysCheckFilesAfterParsing = settings.AlwaysCheckFilesAfterParsing;

                return true;
            }
            catch (Exception) { return false; }
        }
    }
}

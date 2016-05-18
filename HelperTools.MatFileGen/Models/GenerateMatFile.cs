namespace HelperTools.MatFileGen.Models
{
    using Infrastructure.Services;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Prism.Events;
    using Prism.Logging;
    using Properties;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    /// <summary>The GenerateMatFile.</summary>
    public class GenerateMatFile
    {
        #region Fields
        private readonly IUnityContainer unityContainer;
        private readonly IEventAggregator eventAggregator;

        private readonly JsonService jsonService = new JsonService();
        private readonly string configFile = Settings.Default.SettingsMfgGenerellFile;

        private string file;
        private readonly byte[] rgb;
        private readonly bool fromJpg;
        private List<string> matFile;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="GenerateMatFile" /> class.</summary>
        public GenerateMatFile(string fileToGenerate, byte[] rgbInBytes, bool isFromJpg = true)
        {
            unityContainer = ServiceLocator.Current.GetInstance<IUnityContainer>();
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            Extension = ".jpg";

            file = fileToGenerate;
            rgb = rgbInBytes;
            fromJpg = isFromJpg;
            
            var logMessage = $"[{GetType().Name}] is initialized";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets the extension.</summary>
        /// <value>The extension.</value>
        public string Extension { get; set; }

        /// <summary>Gets or sets a value indicating whether the scale should added.</summary>
        /// <value><c>true</c> if the scale should added; otherwise, <c>false</c>.</value>
        public bool AddScale { get; set; }

        /// <summary>Gets or sets the selected scale x.</summary>
        /// <value>The selected scale x.</value>
        public string SelectedScaleX { get; set; }

        /// <summary>Gets or sets the selected scale y.</summary>
        /// <value>The selected scale y.</value>
        public string SelectedScaleY { get; set; }

        /// <summary>Gets or sets the selected scale z.</summary>
        /// <value>The selected scale z.</value>
        public string SelectedScaleZ { get; set; }

        /// <summary>Gets or sets a value indicating whether the rotate should added.</summary>
        /// <value><c>true</c> if the rotate should added; otherwise, <c>false</c>.</value>
        public bool AddRotate { get; set; }

        /// <summary>Gets or sets the selected rotate x.</summary>
        /// <value>The selected rotate x.</value>
        public string SelectedRotateX { get; set; }

        /// <summary>Gets or sets the selected rotate y.</summary>
        /// <value>The selected rotate y.</value>
        public string SelectedRotateY { get; set; }

        /// <summary>Gets or sets the selected rotate z.</summary>
        /// <value>The selected rotate z.</value>
        public string SelectedRotateZ { get; set; }

        /// <summary>Gets or sets a value indicating whether the shi should added.</summary>
        /// <value><c>true</c> if the shi should added; otherwise, <c>false</c>.</value>
        public bool AddShi { get; set; }

        /// <summary>Gets or sets the shi.</summary>
        /// <value>The shi.</value>
        public string Shi { get; set; }

        /// <summary>Gets or sets a value indicating whether the ref should added.</summary>
        /// <value><c>true</c> if the ref should added; otherwise, <c>false</c>.</value>
        public bool AddRef { get; set; }

        /// <summary>Gets or sets the reference.</summary>
        /// <value>The reference.</value>
        public string Ref { get; set; }

        /// <summary>Gets or sets a value indicating whether the tra should added.</summary>
        /// <value><c>true</c> if the tra should added; otherwise, <c>false</c>.</value>
        public bool AddTra { get; set; }

        /// <summary>Gets or sets the tra.</summary>
        /// <value>The tra.</value>
        public string Tra { get; set; }

        /// <summary>Gets or sets a value indicating whether auto should added.</summary>
        /// <value><c>true</c> if auto should added; otherwise, <c>false</c>.</value>
        public bool AddAuto { get; set; }

        /// <summary>Gets or sets a value indicating whether rauto should added.</summary>
        /// <value><c>true</c> if rauto should added; otherwise, <c>false</c>.</value>
        public bool AddRauto { get; set; }

        /// <summary>Gets or sets a value indicating whether the type glass should added.</summary>
        /// <value><c>true</c> if the type glass should added; otherwise, <c>false</c>.</value>
        public bool AddGlass { get; set; }

        /// <summary>Gets or sets a value indicating whether the mirror should added.</summary>
        /// <value><c>true</c> if the mirror should added; otherwise, <c>false</c>.</value>
        public bool AddMirror { get; set; }

        /// <summary>Gets or sets the mirror.</summary>
        /// <value>The mirror.</value>
        public string Mirror { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>Creates the mat file.</summary>
        public void CreateMatFile()
        {
            var filename = CharConverterService.ConvertCharsToAscii(Path.GetFileName(file));

            var colorsDif = new string[3];
            var colorsSpe = new string[3];

            file = file.Replace(Extension, string.Empty);

            for (int i = 0; i < rgb.Length; i++)
            {
                colorsDif[i] = (rgb[i] / 255F).ToString(CultureInfo.InvariantCulture);
                colorsSpe[i] = (rgb[i] / 2550F).ToString(CultureInfo.InvariantCulture);
            }

            matFile = new List<string>
            {
                $"mat {filename.Replace(Extension, string.Empty)}",
                $"dif {colorsDif[0]} {colorsDif[1]} {colorsDif[2]}",
                $"amb {colorsDif[0]} {colorsDif[1]} {colorsDif[2]}",
                $"spe {colorsSpe[0]} {colorsSpe[1]} {colorsSpe[2]}"
            };

            if (fromJpg) matFile.Add($"tex image jpg {filename}");

            AddOptionals();

            FileService.Write($"{file}.mat", matFile);

            var logMessage = $"[{GetType().Name}] \"mat\" file generated";
            unityContainer.Resolve<ILoggerFacade>().Log(logMessage, Category.Debug, Priority.None);
        }

        /// <summary>Adds the optionals.</summary>
        /// <returns></returns>
        private void AddOptionals()
        {
            ReadJson();

            if (AddScale) matFile.Add($"scale {SelectedScaleX} {SelectedScaleY} {SelectedScaleZ}");
            if (AddRotate) matFile.Add($"rotate {SelectedRotateX} {SelectedRotateY} {SelectedRotateZ}");
            if (AddAuto) matFile.Add("auto");
            if (AddRauto) matFile.Add("rauto");
            if (AddGlass) matFile.Add("type glass");
            if (AddMirror) matFile.Add($"mirror {Mirror}");
            if (AddShi) matFile.Add($"shi {Shi}");
            if (AddRef) matFile.Add($"ref {Ref}");
            if (AddTra) matFile.Add($"tra {Tra}");
        }

        private void ReadJson()
        {
            SettingsGenerall settings = jsonService.ReadJson<SettingsGenerall>(configFile);
            AddScale = settings.AddScale;
            SelectedScaleX = settings.SelectedScaleX;
            SelectedScaleY = settings.SelectedScaleY;
            SelectedScaleZ = settings.SelectedScaleZ;
            AddRotate = settings.AddRotate;
            SelectedRotateX = settings.SelectedRotateX;
            SelectedRotateY = settings.SelectedRotateY;
            SelectedRotateZ = settings.SelectedRotateZ;
            AddAuto = settings.AddAuto;
            AddRauto = settings.AddRauto;
            AddGlass = settings.AddGlass;
            AddMirror = settings.AddMirror;
            Mirror = settings.Mirror;
            AddShi = settings.AddShi;
            Shi = settings.Shi;
            AddRef = settings.AddRef;
            Ref = settings.Ref;
            AddTra = settings.AddTra;
            Tra = settings.Tra;
        }
        #endregion Methods
    }
}
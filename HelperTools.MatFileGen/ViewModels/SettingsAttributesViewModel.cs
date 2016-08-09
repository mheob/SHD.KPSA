namespace HelperTools.MatFileGen.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Infrastructure.Base;
    using Infrastructure.Events;
    using Infrastructure.Interfaces;
    using Infrastructure.Services;
    using Microsoft.Practices.Unity;
    using Models;
    using Prism.Logging;
    using Properties;

    /// <summary>The SettingsAttributesViewModel.</summary>
    /// <seealso cref="ViewModelBase" />
    public class SettingsAttributesViewModel : ViewModelBase
    {
        #region Enumeration
        private enum ComboBoxes
        {
            Scale,
            Rotate
        }

        private enum Publisher
        {
            None,
            File,
            SolidRgb
        }
        #endregion Enumeration

        #region Fields
        private readonly JsonService jsonService = new JsonService();
        private readonly string configFile = Settings.Default.SettingsMfgGenerellFile;

        private const string INIT_SCALE = "1.0";
        private const string INIT_ROTATE = "0";
        private const string INIT_MIRROR = "1.0";
        private const string INIT_SHI = "30";
        private const string INIT_REF = "1.0";
        private const string INIT_TRA = "0.75";

        private GenerateMatFile generateFile;
        private string file;
        private byte[] rgb;

        private Publisher publisher;

        private bool addScale;
        private string selectedScaleX;
        private string selectedScaleY;
        private string selectedScaleZ;

        private bool addRotate;
        private string selectedRotateX;
        private string selectedRotateY;
        private string selectedRotateZ;

        private bool addAuto;
        private bool addRauto;
        private bool addGlass;

        private bool addMirror;
        private string mirror;

        private bool addShi;
        private string shi;

        private bool addRef;
        private string _ref;

        private bool addTra;
        private string tra;
        #endregion Fields

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="SettingsAttributesViewModel" /> class.</summary>
        public SettingsAttributesViewModel()
        {
            Mirror = "0";
            Shi = "0";
            Ref = "0";
            Tra = "0";

            publisher = Publisher.None;

            EventAggregator.GetEvent<SelectedMfgFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
            EventAggregator.GetEvent<SolidColorNameUpdateEvent>().Subscribe(OnSolidColorNameUpdateEvent);
            EventAggregator.GetEvent<SolidRgbUpdateEvent>().Subscribe(OnSolidRgbUpdateEvent);
            EventAggregator.GetEvent<SelectedTabUpdateEvent>().Subscribe(OnSelectedTabUpdateEvent);

            WriteJson();
        }
        #endregion Constructor

        #region Properties
        /// <summary>Gets or sets a value indicating whether the scale should added.</summary>
        /// <value><c>true</c> if the scale should added; otherwise, <c>false</c>.</value>
        public bool AddScale
        {
            get { return addScale; }
            set
            {
                if (!SetProperty(ref addScale, value)) return;

                if (string.IsNullOrEmpty(SelectedScaleX) || string.IsNullOrEmpty(SelectedScaleY) || string.IsNullOrEmpty(SelectedScaleZ))
                    InitComboBoxes(ComboBoxes.Scale);

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxScale));
            }
        }

        /// <summary>Gets or sets the ComboBox scale collection.</summary>
        /// <value>The ComboBox scale collection.</value>
        [ExcludeFromCodeCoverage] // TODO: could maybe remove after creating a test of this
        public ObservableCollection<string> ComboBoxScaleCollection => new ObservableCollection<string>()
        {
            "0.1",
            "0.25",
            "0.33",
            "0.5",
            "0.67",
            "0.75",
            "1.0",
            "1.25",
            "1.5",
            "1.75",
            "2.0",
            "2.5"
        };

        /// <summary>Gets or sets the selected scale X.</summary>
        /// <value>The selected scale X.</value>
        public string SelectedScaleX
        {
            get { return selectedScaleX; }
            set
            {
                if (!SetProperty(ref selectedScaleX, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets the selected scale Y.</summary>
        /// <value>The selected scale Y.</value>
        public string SelectedScaleY
        {
            get { return selectedScaleY; }
            set
            {
                if (!SetProperty(ref selectedScaleY, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets the selected scale Z.</summary>
        /// <value>The selected scale Z.</value>
        public string SelectedScaleZ
        {
            get { return selectedScaleZ; }
            set
            {
                if (!SetProperty(ref selectedScaleZ, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets a value indicating whether the rotate should added.</summary>
        /// <value><c>true</c> if  the rotate should added; otherwise, <c>false</c>.</value>
        public bool AddRotate
        {
            get { return addRotate; }
            set
            {
                if (!SetProperty(ref addRotate, value)) return;

                if (string.IsNullOrEmpty(SelectedRotateX) || string.IsNullOrEmpty(SelectedRotateY) || string.IsNullOrEmpty(SelectedRotateZ))
                    InitComboBoxes(ComboBoxes.Rotate);

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxRotate));
            }
        }

        /// <summary>Gets or sets the ComboBox scale collection.</summary>
        /// <value>The ComboBox scale collection.</value>
        [ExcludeFromCodeCoverage] // TODO: could maybe remove after creating a test of this
        public ObservableCollection<string> ComboBoxRotateCollection => new ObservableCollection<string>() {"0", "90"};

        /// <summary>Gets or sets the selected rotate X.</summary>
        /// <value>The selected rotate X.</value>
        public string SelectedRotateX
        {
            get { return selectedRotateX; }
            set
            {
                if (!SetProperty(ref selectedRotateX, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets the selected rotate Y.</summary>
        /// <value>The selected rotate Y.</value>
        public string SelectedRotateY
        {
            get { return selectedRotateY; }
            set
            {
                if (!SetProperty(ref selectedRotateY, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets the selected rotate Z.</summary>
        /// <value>The selected rotate Z.</value>
        public string SelectedRotateZ
        {
            get { return selectedRotateZ; }
            set
            {
                if (!SetProperty(ref selectedRotateZ, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets a value indicating whether auto should added.</summary>
        /// <value><c>true</c> if auto should added; otherwise, <c>false</c>.</value>
        public bool AddAuto
        {
            get { return addAuto; }
            set
            {
                if (!SetProperty(ref addAuto, value)) return;

                if (value) AddRauto = false;

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxAuto));
            }
        }

        /// <summary>Gets or sets a value indicating whether rauto should added.</summary>
        /// <value><c>true</c> if rauto should added; otherwise, <c>false</c>.</value>
        public bool AddRauto
        {
            get { return addRauto; }
            set
            {
                if (!SetProperty(ref addRauto, value)) return;

                if (value) AddAuto = false;

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxRauto));
            }
        }

        /// <summary>Gets or sets a value indicating whether glass should added.</summary>
        /// <value><c>true</c> if glass should added; otherwise, <c>false</c>.</value>
        public bool AddGlass
        {
            get { return addGlass; }
            set
            {
                if (!SetProperty(ref addGlass, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets a value indicating whether mirror should added.</summary>
        /// <value><c>true</c> if mirror should added; otherwise, <c>false</c>.</value>
        public bool AddMirror
        {
            get { return addMirror; }
            set
            {
                if (!SetProperty(ref addMirror, value)) return;

                if (Mirror.Equals("0")) Mirror = INIT_MIRROR;

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxMirror));
            }
        }

        /// <summary>Gets or sets the mirror.</summary>
        /// <value>The mirror.</value>
        public string Mirror
        {
            get { return mirror; }
            set
            {
                if (!SetProperty(ref mirror, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets a value indicating whether shi should added.</summary>
        /// <value><c>true</c> if shi should added; otherwise, <c>false</c>.</value>
        public bool AddShi
        {
            get { return addShi; }
            set
            {
                if (!SetProperty(ref addShi, value)) return;

                if (Shi.Equals("0")) Shi = INIT_SHI;

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxShi));
            }
        }

        /// <summary>Gets or sets the shi.</summary>
        /// <value>The shi.</value>
        public string Shi
        {
            get { return shi; }
            set
            {
                if (!SetProperty(ref shi, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets a value indicating whether ref should added.</summary>
        /// <value><c>true</c> if ref should added; otherwise, <c>false</c>.</value>
        public bool AddRef
        {
            get { return addRef; }
            set
            {
                if (!SetProperty(ref addRef, value)) return;

                if (Ref.Equals("0")) Ref = INIT_REF;

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxRef));
            }
        }

        /// <summary>Gets or sets the ref.</summary>
        /// <value>The ref.</value>
        public string Ref
        {
            get { return _ref; }
            set
            {
                if (!SetProperty(ref _ref, value)) return;

                WriteJson();
            }
        }

        /// <summary>Gets or sets a value indicating whether tra should added.</summary>
        /// <value><c>true</c> if tra should added; otherwise, <c>false</c>.</value>
        public bool AddTra
        {
            get { return addTra; }
            set
            {
                if (!SetProperty(ref addTra, value)) return;

                if (Tra.Equals("0")) Tra = INIT_TRA;

                WriteJson();

                var isChecked = value ? Resources.StatusBarChecked : Resources.StatusBarUnchecked;
                EventAggregator.GetEvent<StatusBarMessageUpdateEvent>().Publish(string.Format(isChecked, Resources.CheckBoxTra));
            }
        }

        /// <summary>Gets or sets the tra.</summary>
        /// <value>The tra.</value>
        public string Tra
        {
            get { return tra; }
            set
            {
                if (!SetProperty(ref tra, value)) return;

                WriteJson();
            }
        }
        #endregion Properties

        #region Methods
        [ExcludeFromCodeCoverage] // TODO: could maybe remove after creating a test of this
        private void InitComboBoxes(ComboBoxes comboBox)
        {
            switch (comboBox)
            {
                case ComboBoxes.Scale:
                    SelectedScaleX = INIT_SCALE;
                    SelectedScaleY = INIT_SCALE;
                    SelectedScaleZ = INIT_SCALE;
                    break;
                case ComboBoxes.Rotate:
                    SelectedRotateX = INIT_ROTATE;
                    SelectedRotateY = INIT_ROTATE;
                    SelectedRotateZ = INIT_ROTATE;
                    break;
                default:
                    var ex = new ArgumentOutOfRangeException(nameof(comboBox), comboBox, null);
                    var logMessage = $"[{GetType().Name}] Exception at {MethodBase.GetCurrentMethod()}: {ex.Message}";
                    Container.Resolve<ILoggerFacade>().Log(logMessage, Category.Exception, Priority.High);
                    throw ex;
            }
        }

        private void WriteJson()
        {
            SettingsGenerall settings = new SettingsGenerall
            {
                AddScale = AddScale,
                SelectedScaleX = SelectedScaleX,
                SelectedScaleY = SelectedScaleY,
                SelectedScaleZ = SelectedScaleZ,
                AddRotate = AddRotate,
                SelectedRotateX = SelectedRotateX,
                SelectedRotateY = SelectedRotateY,
                SelectedRotateZ = SelectedRotateZ,
                AddAuto = AddAuto,
                AddRauto = AddRauto,
                AddGlass = AddGlass,
                AddMirror = AddMirror,
                Mirror = Mirror,
                AddShi = AddShi,
                Shi = Shi,
                AddRef = AddRef,
                Ref = Ref,
                AddTra = AddTra,
                Tra = Tra
            };

            jsonService.WriteJson(settings, configFile);

            if (publisher == Publisher.None) return;

            generateFile = new GenerateMatFile(file, rgb, false);
            generateFile.CreateMatFile(true);
        }

        private void OnSelectedTabUpdateEvent(int tab)
        {
            if (tab == 0)
            {
                publisher = Publisher.File;
                EventAggregator.GetEvent<SelectedMfgFilesUpdateEvent>().Subscribe(OnSelectedFilesUpdateEvent);
            }
            else
            {
                publisher = Publisher.SolidRgb;
                EventAggregator.GetEvent<SolidColorNameUpdateEvent>().Subscribe(OnSolidColorNameUpdateEvent);
                EventAggregator.GetEvent<SolidRgbUpdateEvent>().Subscribe(OnSolidRgbUpdateEvent);
            }
        }

        private void OnSelectedFilesUpdateEvent(ObservableCollection<IFiles> files)
        {
            var firstOrDefault = files.FirstOrDefault();
            if (firstOrDefault != null) file = firstOrDefault.FullFilePath;

            ColorConverterService.GetRgbFromImage(file);
            publisher = Publisher.File;
        }

        private void OnSolidColorNameUpdateEvent(string colorName)
        {
            file = colorName;
            publisher = Publisher.SolidRgb;
        }

        private void OnSolidRgbUpdateEvent(byte[] solidRgb)
        {
            rgb = solidRgb;
            publisher = Publisher.SolidRgb;
        }
        #endregion Methods
    }
}
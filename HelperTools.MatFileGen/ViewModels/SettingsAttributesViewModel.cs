namespace HelperTools.MatFileGen.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Infrastructure.Base;

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
        #endregion Enumeration

        #region Constants
        private const string INIT_SCALE = "1.0";
        private const string INIT_ROTATE = "0";
        private const string INIT_MIRROR = "1.0";
        private const string INIT_SHI = "30";
        private const string INIT_REF = "1.0";
        private const string INIT_TRA = "0.75";
        #endregion Constants

        #region Fields
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
            //Mirror = INIT_MIRROR;
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
                {
                    InitComboBoxes(ComboBoxes.Scale);
                }
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
            set { SetProperty(ref selectedScaleX, value); }
        }

        /// <summary>Gets or sets the selected scale Y.</summary>
        /// <value>The selected scale Y.</value>
        public string SelectedScaleY
        {
            get { return selectedScaleY; }
            set { SetProperty(ref selectedScaleY, value); }
        }

        /// <summary>Gets or sets the selected scale Z.</summary>
        /// <value>The selected scale Z.</value>
        public string SelectedScaleZ
        {
            get { return selectedScaleZ; }
            set { SetProperty(ref selectedScaleZ, value); }
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
                {
                    InitComboBoxes(ComboBoxes.Rotate);
                }
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
            set { SetProperty(ref selectedRotateX, value); }
        }

        /// <summary>Gets or sets the selected rotate Y.</summary>
        /// <value>The selected rotate Y.</value>
        public string SelectedRotateY
        {
            get { return selectedRotateY; }
            set { SetProperty(ref selectedRotateY, value); }
        }

        /// <summary>Gets or sets the selected rotate Z.</summary>
        /// <value>The selected rotate Z.</value>
        public string SelectedRotateZ
        {
            get { return selectedRotateZ; }
            set { SetProperty(ref selectedRotateZ, value); }
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
            }
        }


        /// <summary>Gets or sets a value indicating whether glass should added.</summary>
        /// <value><c>true</c> if glass should added; otherwise, <c>false</c>.</value>
        public bool AddGlass
        {
            get { return addGlass; }
            set { SetProperty(ref addGlass, value); }
        }

        /// <summary>Gets or sets a value indicating whether mirror should added.</summary>
        /// <value><c>true</c> if mirror should added; otherwise, <c>false</c>.</value>
        public bool AddMirror
        {
            get { return addMirror; }
            set
            {
                if (!SetProperty(ref addMirror, value)) return;
                if (string.IsNullOrEmpty(Mirror)) Mirror = INIT_MIRROR;
            }
        }

        /// <summary>Gets or sets the mirror.</summary>
        /// <value>The mirror.</value>
        public string Mirror
        {
            get { return mirror; }
            set { SetProperty(ref mirror, value); }
        }

        /// <summary>Gets or sets a value indicating whether shi should added.</summary>
        /// <value><c>true</c> if shi should added; otherwise, <c>false</c>.</value>
        public bool AddShi
        {
            get { return addShi; }
            set
            {
                if (!SetProperty(ref addShi, value)) return;
                if (string.IsNullOrEmpty(Shi)) Shi = INIT_SHI;
            }
        }

        /// <summary>Gets or sets the shi.</summary>
        /// <value>The shi.</value>
        public string Shi
        {
            get { return shi; }
            set { SetProperty(ref shi, value); }
        }

        /// <summary>Gets or sets a value indicating whether ref should added.</summary>
        /// <value><c>true</c> if ref should added; otherwise, <c>false</c>.</value>
        public bool AddRef
        {
            get { return addRef; }
            set
            {
                if (!SetProperty(ref addRef, value)) return;
                if (string.IsNullOrEmpty(Ref)) Ref = INIT_REF;
            }
        }

        /// <summary>Gets or sets the ref.</summary>
        /// <value>The ref.</value>
        public string Ref
        {
            get { return _ref; }
            set { SetProperty(ref _ref, value); }
        }

        /// <summary>Gets or sets a value indicating whether tra should added.</summary>
        /// <value><c>true</c> if tra should added; otherwise, <c>false</c>.</value>
        public bool AddTra
        {
            get { return addTra; }
            set
            {
                if (!SetProperty(ref addTra, value)) return;
                if (string.IsNullOrEmpty(Tra)) Tra = INIT_TRA;
            }
        }

        /// <summary>Gets or sets the tra.</summary>
        /// <value>The tra.</value>
        public string Tra
        {
            get { return tra; }
            set { SetProperty(ref tra, value); }
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
                    throw new ArgumentOutOfRangeException(nameof(comboBox), comboBox, null);
            }
        }
        #endregion Methods
    }
}
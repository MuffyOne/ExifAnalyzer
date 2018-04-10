using ExifAnalyzer.Common;
using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using ExifAnalyzer.Common.Extensions;

namespace MainModule
{
    public class DirectoryAnalyzerViewModel : BindableBase 
    {

        #region fields
        private string _selectedFolder;
        private bool _startAnalysisEnabled;
        private List<string> _jpegFilesLocation;
        private int _currentProgress;
        private BackgroundWorker analysisWorker;
        private string _curPath;
        private IResultSet _resultSet;
        private IRegionManager _regionManager;
        #endregion

        #region properties
        public ICommand BrowseFolders { get; set; }

        public ICommand StartAnalysis { get; set; }
        

        public string SelectedFolder
        {
            get { return _selectedFolder; }
            set { SetProperty(ref _selectedFolder, value); }
        }

        public bool StartAnalysisEnabled
        {
            get { return _startAnalysisEnabled; }
            set { SetProperty(ref _startAnalysisEnabled, value); }
        }

        public int CurrentProgress
        {
            get { return _currentProgress; }
            set { SetProperty(ref _currentProgress, value); }
        }

        private IRegion MainRegion { get { return _regionManager.Regions[RegionNames.MainRegion]; } }
        #endregion

        #region constructor
        public DirectoryAnalyzerViewModel(IResultSet resultSet, IRegionManager regionManager)
        {
            _jpegFilesLocation = new List<string>();
            InitializeWorker();
            InitializeCommands();
            _resultSet = resultSet;
            _regionManager = regionManager;
        }

        private void InitializeWorker()
        {
            analysisWorker = new BackgroundWorker();
            analysisWorker.DoWork += AnalyzeFiles;
            analysisWorker.ProgressChanged += AnalyzerProgressChanged;
            analysisWorker.RunWorkerCompleted += AnalyzeFilesComplete;
            analysisWorker.WorkerReportsProgress = true;
        }

        private void AnalyzeFilesComplete(object sender, RunWorkerCompletedEventArgs e)
        {   
            _resultSet.GenerateGrouppedList();
            SelectedFolder = string.Format("Analysis of {0} files completed ", _jpegFilesLocation.Count());
            MainRegion.NavigateTo(typeof(ResultsView));

        }

        private void AnalyzerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        private void AnalyzeFiles(object sender, DoWorkEventArgs e)
        {
            var curFileInProgress = 0.0;
            foreach(string filePath in _jpegFilesLocation)
            {
                curFileInProgress++;
                ProcessedPhoto processedPhoto = Analyzer.ProcesImage(filePath);
                _resultSet.AddProcessedPhoto(processedPhoto);
                analysisWorker.ReportProgress((int)((curFileInProgress/_jpegFilesLocation.Count)*100));
            }
           
        }
        #endregion

        #region methods
        private void InitializeCommands()
        {
            BrowseFolders = new DelegateCommand(OnBrowseFolders);
            StartAnalysis = new DelegateCommand(OnStartAnalysis);
        }

        private void OnStartAnalysis()
        {
            StartAnalysisEnabled = false;
            if (_resultSet.GetNumberOfAnalyzedPictures() > 0)
            {
                var clear = MessageBox.Show("Do you want to clear any prevoius analized image?", "Clear resutls?", MessageBoxButtons.YesNo);
                if (clear == DialogResult.Yes)
                {
                    _resultSet.ClearPreviousResults();
                }
            }
            SelectedFolder = string.Format("Analyzing {0} files on folder {1} ", _jpegFilesLocation.Count(), _curPath);
            analysisWorker.RunWorkerAsync();
        }

        private void OnBrowseFolders()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            _curPath = dialog.SelectedPath;
            GetJpegFiles(_curPath);
            SelectedFolder = string.Format("You are about to analyze folder: {0}, found  {1} Jpegs files", _curPath, _jpegFilesLocation.Count());
        }

        private void GetJpegFiles(string folderPath)
        {
            var files = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".jpeg") || s.EndsWith(".jpg") || s.EndsWith(".JPG") || s.EndsWith(".JPEG"));
            _jpegFilesLocation = files.ToList();
            StartAnalysisEnabled = _jpegFilesLocation.Count > 0;
        }
        #endregion
    }
}

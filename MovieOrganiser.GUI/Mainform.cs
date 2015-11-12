using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieOrganiser.Model;
using MovieOrganiser.Helpers;

namespace MovieOrganiser.GUI
{
    public partial class Mainform : Form
    {
        private List<Movie> myfilms = new List<Movie>();

        private SettingsManager sm;
        private Library _movieLibrary;

        public Mainform()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // load settings
            sm = new SettingsManager(SettingsPersistance.LocalXmlFile);
            if (sm == null)
            {
                MessageBox.Show("Instellingen kunnen niet worden ingelezen." + Environment.NewLine + "Programma kan niet worden gestart.", "Fout", MessageBoxButtons.OK);
                Application.Exit();
            }

            // create a moviecollection
            _movieLibrary = new Library();

            // update Title
            this.Text = "Movie Organiser (" + sm.MovieFolder + ")";

            // when form is finished loading, scan folder
            ScanFolder();
        }

        // read folder contents and put all directory names in the listbox
        private void ScanFolder()
        {
            // clear listbox
            listBox1.Items.Clear();

            FolderIterator fi = new FolderIterator(sm.MovieFolder);
            List<Movie> movies = fi.GetMovies();

            // put all the movies in the library
            _movieLibrary.Movies.AddRange(movies);

            // update the listbox with all movies
            listBox1.Items.AddRange(_movieLibrary.Movies.ToArray<Movie>() );

            
           
        }

        

        private void identifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identifySelectedMovie();
        }

        // take name of selected item in listBox1 and send it to TheMovieDb and retrieve possible movies
        private void identifySelectedMovie()
        {
            // check if an item is selected, if not then show error and leave
            if (listBox1.SelectedItem == null )
            {
                MessageBox.Show("Selecteer eerst een film", "Fout", MessageBoxButtons.OK);
                return;
            }

            Movie selectedItem = (Movie) listBox1.SelectedItem;

            TMDB t = new TMDB();
            Movie m = t.GuessMovieandAskUser(selectedItem.Title);

            if ( m != null)
            {
                selectedItem.Title = m.Title;
                selectedItem.IMDBMovieId = m.IMDBMovieId;
                selectedItem.ReleaseDate = m.ReleaseDate;

                // update listbox
                listBox1.Items[listBox1.SelectedIndex] = selectedItem;

                // write NFO file
                NFOFile.Write(selectedItem);
            }
            
            
        }

        private void writeMovieIdToFile(string id, string folderName)
        {
            File.WriteAllText(folderName + "\\movie.text", id);
        }

        private void identifyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            identifySelectedMovie();
        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = folderBrowserDialog1.ShowDialog();

            if (r == DialogResult.OK)
            {
                // user clicked OK, so apply the new setting

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack
{
    public class Song
    {

        int key, mode, timeSignature, year;
        float duration, energy, loudness, tempo;
        string trackID, artistID, artistName, genre, release, songID, title;

        public Song(float duration, float energy, int key, float loudness, int mode, float tempo, int timeSignature, string trackID, string artistID, string artistName, string genre, string release, string songID, string title, int year)
        {
            this.duration = duration;
            this.energy = energy;
            this.key = key;
            this.loudness = loudness;
            this.mode = mode;
            this.tempo = tempo;
            this.timeSignature = timeSignature;
            this.trackID = trackID;
            this.artistID = artistID;
            this.artistName = artistName;
            this.genre = genre;
            this.release = release;
            this.songID = songID;
            this.title = title;
            this.year = year;

        }

        //Getters and Setters
        public int Key
        {
            get
            {
                return key;
            }

            set
            {
                this.key = value;
            }
        }

        public int Mode
        {
            get
            {
                return mode;
            }

            set
            {
                this.mode = value;
            }
        }

        public int TimeSignature
        {
            get
            {
                return timeSignature;
            }

            set
            {
                this.timeSignature = value;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                this.year = value;
            }
        }

        public float Duration
        {
            get
            {
                return duration;
            }

            set
            {
                this.duration = value;
            }
        }

        public float Energy
        {
            get
            {
                return energy;
            }

            set
            {
                this.energy = value;
            }
        }

        public float Loudness
        {
            get
            {
                return loudness;
            }

            set
            {
                this.loudness = value;
            }
        }

        public float Tempo
        {
            get
            {
                return tempo;
            }

            set
            {
                this.tempo = value;
            }
        }

        public string TrackID
        {
            get
            {
                return trackID;
            }

            set
            {
                this.trackID = value;
            }
        }

        public string ArtistID
        {
            get
            {
                return artistID;
            }

            set
            {
                this.artistID = value;
            }
        }

        public string ArtistName
        {
            get
            {
                return artistName;
            }

            set
            {
                this.artistName = value;
            }
        }

        public string Genre
        {
            get
            {
                return genre;
            }

            set
            {
                this.genre = value;
            }
        }

        public string Release
        {
            get
            {
                return release;
            }

            set
            {
                this.release = value;
            }
        }

        public string SongID
        {
            get
            {
                return songID;
            }

            set
            {
                this.songID = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                this.title = value;
            }
        }

    }
}

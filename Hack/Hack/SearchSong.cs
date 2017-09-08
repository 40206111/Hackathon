//Searches for matches according to the number of input fields
// up to 50 results
using System;
using System.Collections.Generic;

namespace Hack
{
    public class SearchSong
    {
        public static List<Song> Search(string artistName, string albumName, string songName){
        List<Song> resultsList = new List<Song>();
        int limit = 50;
        //checked
        if (!songName.Equals(""))
        {
            foreach(Song s in ImportData.songs){
                if(s.Title.ToLower().Contains(songName.ToLower())){
                    resultsList.Add(s);
                }
            }
        }

        //Checked
        if (!albumName.Equals(""))
        {
            if (songName.Equals(""))
            {
                foreach (Song s in ImportData.songs)
                {
                    if (s.Release.ToLower().Contains(albumName.ToLower()))
                    {
                        resultsList.Add(s);
                    }
                }
            }
            else
            {
                foreach (Song s in resultsList)
                {
                    if (!s.Release.ToLower().Contains(albumName.ToLower()))
                    {
                        resultsList.Remove(s);
                    }
                }
            }
        }

            //checked
        if (!artistName.Equals(""))
        {
            if (songName.Equals("") && albumName.Equals(""))
            {
                foreach (Song s in ImportData.songs)
                {
                    if (s.Release.ToLower().Contains(artistName.ToLower()))
                    {
                        resultsList.Add(s);
                    }
                }
            }
            else
            {
                foreach (Song s in resultsList)
                {
                    if (!s.Release.ToLower().Contains(albumName.ToLower()))
                    {
                        resultsList.Remove(s);
                    }
                }
            }
        }

        int listLength = resultsList.Count;
        if (listLength > limit)
        {
            for(int i = limit; i<listLength; i++){
                resultsList.RemoveAt(limit);
            }
        }
        
        //add several names to the list
        return resultsList;
    }


    }
}

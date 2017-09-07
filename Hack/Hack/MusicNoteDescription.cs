namespace NoteValues

{
    public class NoteValues

    {
        //(int MusicNoteEnum

        float[] noteSet = new float[4];
        //int barLength = tempo / 4;
        //float time;

        //Gets duration of a single note
        public float[] GetDuration(float tempo)
        {
            noteSet[(int)NoteValueEnum.Semibreve] = (60/tempo)*4.0f;
            noteSet[(int)NoteValueEnum.Minum] = noteSet[(int)NoteValueEnum.Semibreve] / 2.0f;
            noteSet[(int)NoteValueEnum.Crotchet] = noteSet[(int)NoteValueEnum.Minum] / 2.0f;
            noteSet[(int)NoteValueEnum.Quaver] = noteSet[(int)NoteValueEnum.Crotchet] / 2.0f;
            return noteSet;
        }

        //highest absolute value
        //scale all to be one


        
    }
}
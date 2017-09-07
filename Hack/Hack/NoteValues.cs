namespace Hack

{
    public static class NoteValues
    {
        //Gets duration of the different note types
        public static float[] GetDuration(float tempo)
        {
            float[] noteSet = new float[4];
            noteSet[(int)NoteValueEnum.Semibreve] = (60 / tempo) * 4.0f;
            noteSet[(int)NoteValueEnum.Minum] = noteSet[(int)NoteValueEnum.Semibreve] / 2.0f;
            noteSet[(int)NoteValueEnum.Crotchet] = noteSet[(int)NoteValueEnum.Minum] / 2.0f;
            noteSet[(int)NoteValueEnum.Quaver] = noteSet[(int)NoteValueEnum.Crotchet] / 2.0f;
            return noteSet;
        }
    }
}
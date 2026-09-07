using System.Collections;



namespace TitanCollectiveStudios.Commons.Commands
{
    public interface iCommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        public void Execute();


        /// <summary>
        /// Execute the command syncronous
        /// Default: Will execute (Execute()) method and wait next frame
        /// </summary>
        public IEnumerator WaitExecute();

        /// <summary>
        /// Undo the command (optional)
        /// </summary>
        public void Undo();
    }
}
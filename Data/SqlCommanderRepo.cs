using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if(cmd == null){
                throw new ArgumentNullException(nameof (cmd));
            }
            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            
            if(cmd == null){
                throw new ArgumentNullException(nameof (cmd));
            }
            _context.Commands.Remove(cmd);
        }

        public IEnumerable<Command> GetAppCommands()
        {
            return _context.Commands.ToList();
            // ToList wil return all commands in the Db();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        // When changes is made  on the dbContext you'll have to call this method to save in the database as well
        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);  
        }

        public void UpdateCommand(Command cmd)
        {
            // return
        }
    }
}
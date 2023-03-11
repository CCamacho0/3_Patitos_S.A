using Microsoft.EntityFrameworkCore;

namespace _3_Patitos_S.A.Data
{
    public class Db_Context: DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> options) : base(options)
        {

        }

        //Aca se crean los contructores de los modelos

    }
}

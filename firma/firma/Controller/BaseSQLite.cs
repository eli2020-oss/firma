﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace firma.Controller
{
    public class BaseSQLite
    {
        readonly SQLiteAsyncConnection db;

        //constructor de la clase DataBaseSQLite
        public BaseSQLite(String pathdb)
        {
            //crear una conexion a la base de datos
            db = new SQLiteAsyncConnection(pathdb);

            //creacion de la tabla personas dentro de SQLite
            db.CreateTableAsync<Models.firma>().Wait();
        }

        //opaciones CRUD con SQLite
        //READ List Way
        public Task<List<Models.firma>> ObtenerListaFirmas()
        {
            return db.Table<Models.firma>().ToListAsync();

        }

        //retornar una persona //READ one by one
        //(retorna el primero que encuentre ya que pueden a ver varios del dp choluteca)
        public Task<Models.firma> ObtenerFirma(int pcodigo)
        {
            return db.Table<Models.firma>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GrabarFirma(Models.firma firma)
        {
            if (firma.codigo != 0)
            {
                return db.UpdateAsync(firma);
            }
            else
            {

                return db.InsertAsync(firma);
            }
        }

        //delete
        public Task<int> EliminarUbicacion(Models.firma firma)
        {
            return db.DeleteAsync(firma);
        }
    }
}

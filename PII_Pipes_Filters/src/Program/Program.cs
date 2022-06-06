using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creo la variable donde voy a traer la imagen
            PictureProvider provider = new PictureProvider(); 

            //Guardo la imagen en "picture"
            IPicture picture = provider.GetPicture(@"luke.jpg"); 


            //Creo una instancia de Pipe Null que me va a devolver la imagen final
            IPipe pipeNull = new PipeNull(); 

            //Creo una instancia de Filtro Negativo
            IFilter filterNegative = new FilterNegative();

            //Creo una instancia de Filtro Save
            IFilter filterSave = new FilterSave();

            //Creo otra instancia de PipeSerial que va a aplicar el segundo filtro
            IPipe pipeSerial3 = new PipeSerial (filterNegative,pipeNull);
            
            //Creo otra instancia de PipeSerial que va a gaurdar la imagen con el primer filtro
            IPipe pipeSerial2 = new PipeSerial (filterSave,pipeSerial3);

            //Creo una instancia de Filtro Escala de Grises
            IFilter filterGreyScale = new FilterGreyscale();

            //Creo una instancia de PipeSerial con el primer filtro y el siguiente pipe al que va
            IPipe pipeSerial1 = new PipeSerial (filterGreyScale,pipeSerial2);
           
            //Comienzo de secuencia (el mismo Pipe realiza el Send al siguiente, por eso se llama por única vez)
            picture = pipeSerial1.Send(picture);

            //Ya se generó provider al comienzo
            provider.SavePicture(picture, @"lukefiltrado.jpg");
        }
    }
}

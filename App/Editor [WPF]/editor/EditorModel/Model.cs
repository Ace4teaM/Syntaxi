using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EditorModel.Entity;
using Lib;

namespace EditorModel
{
    public class Model : EntitiesModel
    {
        // version interne du model de fichier
        // incrémenté à chaque modification du model
        private Int32 version = 1;

        // Etat en cours
        public EditorStates states;

        public override IEntity CreateEntity(string Name)
        {
            Type t = Type.GetType("EditorModel.Entity." + Name + ", EditorModel");
            IEntity entity = Activator.CreateInstance(t) as IEntity;
            entity.Model = this;

            return entity;
        }

        public void CreateModel()
        {
            states = new EditorStates(version.ToString(), String.Empty);
            this.Add(states);
        }

        public void AddCppStates()
        {
            // function example
            states.AddEditorSampleCode(new EditorSampleCode(
@"
/**
	Alloue est initialise la mémoire

	Parametres:
		handle_count : nombre d'handle allouable
		handle_size  : taille d'un handle

	Retourne:
		1 en cas de succes, 0 en cas d'erreur.
*/
ushort npInitHandle(ushort handle_count,ushort handle_size)
{ ... }
",
                @"function")
            );

            // struct example
            states.AddEditorSampleCode(new EditorSampleCode(
@"
/**
	En-tete d'un handle
*/
typedef struct _NP_HANDLE_HEADER{
	ushort chunk_count;
	ushort chunk_size;
	size_t data_size;
	NP_HANDLE_INDICE* index;
	char* data;
	char* lock_data;
}NP_HANDLE_HEADER;
",
                @"struct")
            );
        }

        public bool LoadFromFile(String Filename)
        {
            // Charge les infos sur le projet
            if (File.Exists(Filename))
            {
                FileStream file = File.Open(Filename, FileMode.Open);
                BinaryReader reader = new BinaryReader(file);
                states = new EditorStates();
                this.Add(states);
                states.ReadBinary(reader, null);
                reader.Close();
                file.Close();
                return true;
            }

            return false;
        }

        public bool SaveToFile(String Filename){
            if (states != null)
            {
                FileStream file = File.Open(Filename, FileMode.OpenOrCreate);
                BinaryWriter reader = new BinaryWriter(file);
                states.WriteBinary(reader);
                reader.Close();
                file.Close();
                return true;
            }

            return false;
        }
    }
}

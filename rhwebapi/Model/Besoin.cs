namespace rhwebapi.Models;
using Npgsql;
using System.Collections.Generic ; 
    public class Besoin
    {
        int idbesoin ;
        int idannonce;
        int idsouscritere ;
        float note;

        public Besoin(){}
        public Besoin(int idbesoin,int idannonce,int idsouscritere,int note  ){
            setIdbesoin(idbesoin);
            setIdannonce(idannonce);
            setIdsouscritere(idsouscritere);
            setNote(note);
        }
        public Besoin(string idbesoin,string idannonce,string idsouscritere,string note  ){
            setIdbesoin(idbesoin);
            setIdannonce(idannonce);
            setIdsouscritere(idsouscritere);
            setNote(note);
        }

        public int getIdbesoin(){
            return this.idbesoin;
        }
        public void setIdbesoin(int idbesoin){
            this.idbesoin=idbesoin;
        }
        public void setIdbesoin(string idbesoin){
            try{ setIdbesoin(int.Parse(idbesoin));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idbesoin+", n'est pas entier"); }
        }
        public int getIdannonce(){
            return this.idannonce;
        }
        public void setIdannonce(int idannonce){
            this.idannonce=idannonce;
        }
        public void setIdannonce(string idannonce){
            try{ setIdannonce(int.Parse(idannonce));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idannonce+", n'est pas entier"); }
        }

        public int getIdsouscritere(){
            return this.idsouscritere;
        }
        public void setIdsouscritere(int idsouscritere){
            this.idsouscritere=idsouscritere;
        }
        public void setIdsouscritere(string idsouscritere){
            try{ setIdsouscritere(int.Parse(idsouscritere));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idsouscritere+", n'est pas entier"); }
        }

        public float getNote(){
            return this.note;
        }
        public void setNote(float note){
            if(note<0){ throw new Exception("note doit etre > ou = 0");  }
            this.note=note;
        }
        public void setNote(string note){
            try{ setNote(int.Parse(note));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(note+", n'est pas entier"); }
        }

        public void save(NpgsqlConnection connexion){
            //test de verification d'existance de l'id
            //...      
            string query="insert into Besoin(idannonce,idsouscritere,note) values ("+this.idannonce+","+this.idsouscritere+","+this.note+")";
            Connection connect=new Connection();
            connect.ExecuteNotSelectQuery(connexion,query);
        }
        public Besoin getById(NpgsqlConnection connexion,int idbesoin){
            string query="select * from besoin where idbesoin="+idbesoin+" order by idbesoin ASC";
            Connection connect=new Connection(); 
            List<Dictionary<string, object>>? result = connect.ExecuteSelectQuery(connexion, query);
            if(result==null){ return null; }
            Besoin besoin=null;
            int i=0;
            foreach (var row in result){ 
                besoin=new Besoin(row["idbesoin"].ToString(), row["idannonce"].ToString() ,row["idsouscritere"].ToString() ,row["note"].ToString() );
                i++;
            }
            return besoin;
        }

    }
namespace rhwebapi.Models;
using Npgsql;
using System.Collections.Generic ; 
    public class Calendarjob
    {
        int idcalendarjob ;
        int idannonce ;
        int idjoursemaine ;

        public Calendarjob(){}
        public Calendarjob(int idcalendarjob,int idannonce,int idjoursemaine){
            setIdcalendarjob(idcalendarjob);
            setIdannonce(idannonce);
            setIdjoursemaine(idjoursemaine);
        }
        public Calendarjob(string idcalendarjob,string idannonce,string idjoursemaine){
            setIdcalendarjob(idcalendarjob);
            setIdannonce(idannonce);
            setIdjoursemaine(idjoursemaine);
        }
        public int getIdcalendarjob(){
            return this.idcalendarjob;
        }
        public void setIdcalendarjob(int idcalendarjob){
            this.idcalendarjob=idcalendarjob;
        }
        public void setIdcalendarjob(string idcalendarjob){
            try{ setIdcalendarjob(int.Parse(idcalendarjob));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idcalendarjob+", n'est pas entier"); }
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

        public int getIdjoursemaine(){
            return this.idjoursemaine;
        }
        public void setIdjoursemaine(int idjoursemaine){
            this.idjoursemaine=idjoursemaine;
        }
        public void setIdjoursemaine(string idjoursemaine){
            try{ setIdjoursemaine(int.Parse(idjoursemaine));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idjoursemaine+", n'est pas entier"); }
        }


    // Calendarjob
    // {
    //     int idcalendarjob ;
    //     int idannonce ;
    //     int idjoursemaine ;
        public void save(NpgsqlConnection connexion){
            //test de verification d'existance de l'id
            //...
            string query="insert into calendarjob(idannonce,idjoursemaine) values ("+this.idannonce+","+this.idjoursemaine+")";
            Connection connect=new Connection();
            connect.ExecuteNotSelectQuery(connexion,query);
        }
        public Calendarjob getById(NpgsqlConnection connexion,int idCalendarjob){
            string query="select * from Calendarjob where idcalendarjob="+idcalendarjob+" order by idcalendarjob ASC";
            Connection connect=new Connection(); 
            List<Dictionary<string, object>>? result = connect.ExecuteSelectQuery(connexion, query);
            if(result==null){ return null; }
            Calendarjob calendarjob=null;
            int i=0;
            foreach (var row in result){ 
                calendarjob=new Calendarjob(row["idcalendarjob"].ToString(),row["idannonce"].ToString() ,row["idjoursemaine"].ToString() );
                i++;
            }
            return calendarjob;
        }
        
    }
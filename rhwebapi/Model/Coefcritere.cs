namespace rhwebapi.Models;
using Npgsql;
using System.Collections.Generic ; 
    public class Coefcritere
    {
        int idcoefcritere; 
        int idcritere ;
        int idannonce ;
        int coeficient ;

        public Coefcritere(){}
        public Coefcritere(int idcoefcritere,int idcritere, int idannonce,int coeficient  ){
            setIdcoefcritere(idcoefcritere);
            setIdcritere(idcritere);
            setIdannonce(idannonce);
            setCoeficient(coeficient);
        }
        public Coefcritere(string idcoefcritere,string idcritere, string idannonce,string coeficient  ){
            setIdcoefcritere(idcoefcritere);
            setIdcritere(idcritere);
            setIdannonce(idannonce);
            setCoeficient(coeficient);
        }

        public int getIdcoefcritere(){
            return this.idcoefcritere;
        }
        public void setIdcoefcritere(int idcoefcritere){
            this.idcoefcritere=idcoefcritere;
        }
        public void setIdcoefcritere(string idcoefcritere){
            try{ setIdcoefcritere(int.Parse(idcoefcritere));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idcoefcritere+", n'est pas entier"); }
        }

        public int getIdcritere(){
            return this.idcritere;
        }
        public void setIdcritere(int idcritere){
            this.idcritere=idcritere;
        }
        public void setIdcritere(string idcritere){
            try{ setIdcritere(int.Parse(idcritere));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idcritere+", n'est pas entier"); }
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

        public int getCoeficient(){
            return this.coeficient;
        }
        public void setCoeficient(int coeficient){
            this.coeficient=coeficient;
        }
        public void setCoeficient(string coeficient){
            try{ setCoeficient(int.Parse(coeficient));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(coeficient+", n'est pas entier"); }
        }


        public void save(NpgsqlConnection connexion){
            //test de verification d'existance de l'id
            //...
            string query="insert into coefcritere(idcritere,idannonce,coeficient) values ("+this.idcritere+","+this.idannonce+","+this.coeficient+")";
            Connection connect=new Connection();
            connect.ExecuteNotSelectQuery(connexion,query);
        }
        public Coefcritere getById(NpgsqlConnection connexion,int idcoefcritere){
            string query="select * from Coefcritere where idcoefcritere="+idcoefcritere+" order by idcoefcritere ASC";
            Connection connect=new Connection(); 
            List<Dictionary<string, object>>? result = connect.ExecuteSelectQuery(connexion, query);
            if(result==null){ return null; }
            Coefcritere coefcritere=null;
            int i=0;
            foreach (var row in result){ 
                coefcritere=new Coefcritere(row["idcoefcritere"].ToString(), row["idcritere"].ToString() ,row["idannonce"].ToString() ,row["coeficient"].ToString() );
                i++;
            }
            return coefcritere;
        }
    }
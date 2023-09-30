namespace rhwebapi.Models;
using Npgsql;
using System.Collections.Generic ; 
    public class Annonce
    {
        int idannonce;
        int idservice;
        int estdispo ;
        DateTime dateannonce;
        float heurejournalier;
        float heurehebdomadaire;

        public Annonce(){}
        public Annonce(int idannonce,int idservice,int estdispo,DateTime dateannonce ,float heurejournalier,float heurehebdomadaire ){
            setIdannonce(idannonce);
            setIdservice(idservice);
            setEstdispo(estdispo);
            setDateannonce(dateannonce);
            setHeurejournalier(heurejournalier);
            setHeurehebdomadaire(heurehebdomadaire);
        }
        public Annonce(string idannonce,string idservice,string estdispo,string dateannonce ,string heurejournalier,string heurehebdomadaire ){
            setIdannonce(idannonce);
            setIdservice(idservice);
            setEstdispo(estdispo);
            setDateannonce(dateannonce);
            setHeurejournalier(heurejournalier);
            setHeurehebdomadaire(heurehebdomadaire);
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

        public int getIdservice(){
            return this.idservice;
        }
        public void setIdservice(int idservice){
            this.idservice=idservice;
        }
        public void setIdservice(string idservice){
            try{ setIdservice(int.Parse(idservice));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(idservice+", n'est pas entier"); }
        }

        public int getEstdispo(){
            return this.estdispo;
        }
        public void setEstdispo(int estdispo){
            this.estdispo=estdispo;
        }
        public void setEstdispo(string estdispo){
            try{ setEstdispo(int.Parse(estdispo));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(estdispo+", n'est pas entier"); }
        }

        public DateTime getDateannonce(){
            return this.dateannonce;
        }
        public void setDateannonce(DateTime dateannonce){
            this.dateannonce=dateannonce;
        }
        public void setDateannonce(string dateannonce){
            try{ setDateannonce( DateTime.Parse(formaterDateinDateTime("yyyy-MM-dd",dateannonce +" 00:00:00")) );
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(dateannonce+", n'est pas entier"); }
        }

        public float getHeurejournalier(){
            return this.heurejournalier;
        }
        public void setHeurejournalier(float heurejournalier){
            this.heurejournalier=heurejournalier;
        }
        public void setHeurejournalier(string heurejournalier){
            try{ setHeurejournalier(float.Parse(heurejournalier));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(heurejournalier+", n'est pas entier"); }
        }

        public float getHeurehebdomadaire(){
            return this.heurehebdomadaire;
        }
        public void setHeurehebdomadaire(float heurehebdomadaire){
            this.heurehebdomadaire=heurehebdomadaire;
        }
        public void setHeurehebdomadaire(string heurehebdomadaire){
            try{ setHeurehebdomadaire(float.Parse(heurehebdomadaire));
            }catch(Exception ex){ Console.WriteLine(ex); throw new Exception(heurehebdomadaire+", n'est pas entier"); }
        }
        public string formaterDateinDateTime(string formatdate,string date){
            string[] dtH=date.Split(" "); //[0]="2021-09-12"  /  [1]="12:00:00"
            if(dtH.Length<1){ throw new Exception("date time "+date+" non valide"); }
            string dt=dtH[0];
            string[] frms=formatdate.Split("-"); //"yyyy MM dd" ou "dd MM yyyy"
            string[] dts=dt.Split("-"); //"12 09 2021   ou   2021 09 12
            string splitage="-";
            if(frms.Length!=3){  
                    frms=formatdate.Split("/");
                    splitage="/";
                    if(frms.Length!=3){  throw new Exception("format "+formatdate+" non valide"); }
            }
            if(dts.Length!=3){  
                    dts=dt.Split("/");
                    if(dts.Length!=3){  throw new Exception("date "+date+" non valide"); }
            }
            string dtrep=dts[0]+""+splitage+""+dts[1]+""+splitage+""+dts[2];
            if(frms[0].Length==2 && frms[2].Length==4){ //dd-MM-yyyy
                if(dts[0].Length==4){ dtrep=dts[2]+""+splitage+""+dts[1]+""+splitage+""+dts[0]; }
            }else if(frms[0].Length==4 && frms[2].Length==2){//yyyy-MM-dd
                if(dts[2].Length==4){ dtrep=dts[2]+""+splitage+""+dts[1]+""+splitage+""+dts[0];}
            }else{
            throw new Exception("format "+formatdate+" non valide"); 
            }
            if(dtH.Length>=2){
                dtrep=dtrep+" "+dtH[1];
            }
            Console.WriteLine("------------------------------>"+dtrep);
            return dtrep;
        }

        public void save(NpgsqlConnection connexion){
            //test de verification d'existance de l'id
            //...
            string query="insert into annonce(idservice,estdispo,dateannonce,heurejournalier,heurehebdomadaire) values ("+this.idservice+","+this.estdispo+","+this.dateannonce+","+this.heurejournalier+","+this.heurehebdomadaire+")";
            Connection connect=new Connection();
            connect.ExecuteNotSelectQuery(connexion,query);
        }
        public Annonce getById(NpgsqlConnection connexion,int idannonce){
            string query="select * from annonce where idannonce="+idannonce+" order by idannonce ASC";
            Connection connect=new Connection();
            List<Dictionary<string, object>>? result = connect.ExecuteSelectQuery(connexion, query);
            if(result==null){ return null; }
            Annonce annonce=null;
            int i=0;
            foreach (var row in result){ 
                annonce=new Annonce(row["idannonce"].ToString(), row["idservice"].ToString() ,row["estdispo"].ToString() ,row["dateannonce"].ToString() ,row["heurejournalier"].ToString() ,row["heurehebdomadaire"].ToString());
                i++;
            }
            return annonce;
        }

        public void insertAllDescriptionBesoin(NpgsqlConnection connexion, string[] calendarjob, string heurehebdomadaire, string heurejournalier,string idservice, string idsscrtdiplom, string coefdip, string idsscrtexp, string coefexp, string[] idsscrtstatu, string[] notestatu,string coefstatu, string[] idsscrtnation, string[] notenation, string coefnation, string[] idsscrtgenre, string[] notegenre, string coefgenre){
            DateTime datenow=DateTime.Today;
            //test de l'existance de chaque id foreign key
            Connection connect=new Connection();
            //verifiena hoe mi-existe anaty base de donnee ve les choix nosafidiny (diplome,nationalite etc....)  : 
            //Existance service
            bool existId=connect.isExist(connexion, "service", "idservice", idservice, false);
            if(existId==false){ throw new Exception("service ("+idservice+")"+" introuvable"); }
            existId=connect.isExist(connexion, "souscritere", "where idcritere=(select idcritere from critere where nomcritere='diplome') and idsouscritere="+idsscrtdiplom);
            if(existId==false){ throw new Exception("diplome ("+idsscrtdiplom+")"+" introuvable"); }

            //Existance situation matrimonial
            for(int i=0;i<idsscrtstatu.Length;i++){
                existId=connect.isExist(connexion, "souscritere", "where idcritere=(select idcritere from critere where nomcritere='situation matrimoniale') and idsouscritere="+idsscrtstatu[i]);
                if(existId==false){ throw new Exception("situation matrimonial ("+idsscrtstatu[i]+")"+"introuvable"); }
            }
            
            //Existance nationalite
            for(int i=0;i<idsscrtnation.Length;i++){
                existId=connect.isExist(connexion, "souscritere", "where idcritere=(select idcritere from critere where nomcritere='nationalite') and idsouscritere="+idsscrtnation[i]);
                if(existId==false){ throw new Exception("nationalite ("+idsscrtnation[i]+")"+"introuvable"); }
            }

            //Existance genre
            for(int i=0;i<idsscrtgenre.Length;i++){
                existId=connect.isExist(connexion, "souscritere", "where idcritere=(select idcritere from critere where nomcritere='genre') and idsouscritere="+idsscrtgenre[i]);
                if(existId==false){ throw new Exception("nationalite ("+idsscrtgenre[i]+")"+"introuvable"); }
            }

            //string[] idsscrtgenre, string[] notegenre, string coefgenre
            
            //for(int i=0;i<)
            //isExist(NpgsqlConnection connection, "souscritere", "where idsouscritere=")
            //Annonce(string idannonce,string idservice,string estdispo,string dateannonce ,string heurejournalier,string heurehebdomadaire )  
            Annonce annonce=new Annonce("0",idservice,"0", datenow.ToString() ,heurejournalier, heurehebdomadaire );
        }
    }
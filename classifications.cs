using System;

namespace classifications{

//This class is for the section one classifications
class SectionOne{

public int validate(string sect1){
        
        int status;
//This switch case statement will assign the variable "status" with a value from 1-4 starting from TOP SECRET. The variable status willbe assigned with 0 if the classification was invalid.
        switch(sect1){

            case "TOP SECRET":
            

            status = 1;
            
            break;

            case "SECRET":

          
            status = 2;
            

            break;

            case "CONFIDENTIAL":

           
            status = 3;
        

            break;

            case "UNCLASSIFIED":
         
            status = 4;
       
            break;

            default:
       
            status = 0;
        
            break;

        }
     
        return status;
        
    }

}

//This class is for the section 2 classifications
class SectionFour{

    public int validate(string sect1,string sect4, string noforn, string orcon, string tk){
        
        int status = 0;

//This if statement checks if the variable "sect1" is not assigned with UNCLASSIFIED. otherwise, the variable status will be assigned with 0, which is invalid.
        if(sect1 != "UNCLASSIFIED"){

//This if statement checks if "sect2" is assigned with "HCS" as well checking if noforn is assigned with "NOFORN"
        if((sect4 == "HCS")&&((noforn == "NOFORN")||(noforn == "NOT RELEASABLE TO FOREIGN NATIONALS"))){

            
            status = 1;
           

//This will check if "sect2" is assigned with either COMINT or SI
        }else if((sect4 == "COMINT") || (sect4 == "SI")){

            status = 2;




//This will check if "sect2" is assigned with COMINT-GAMMA or SI-G as well as checking if "sect1" is assigned with TOP SECRET
        }else if(((sect4 == "COMINT-GAMMA") || (sect4 == "SI-G")) && (sect1 == "TOP SECRET")){
//this will check if the classification ORCON exists
            if((orcon == "ORCON")||(orcon == "ORIGINATOR CONTROLLED")){
       
            status = 3;
      
//This will also check if TALENT KEYHOLE exists as well. If it does, the value of status will be increased by 3
             if((tk == "TALENT KEYHOLE") || (tk == "TK")){
          
            status += 3;
         
            
            }

            }

           

            

//This will check if "sect2" is assigned with COMINT-ECI or SI-ECI as well as checking if "sect1" is assigned with TOP SECRET.
        }else if( ((sect4 == "COMINT-ECI") || (sect4 == "SI-ECI")) && (sect1 == "TOP SECRET") ){

            status = 4;
//This will also check if TALENT KEYHOLE exists. If it does, the value of status will be increased by 3
            if((tk == "TALENT KEYHOLE") || (tk == "TK")){
         
            status += 3;
      
            
            }



//This will check if "sect2" is assigned with COMINT-GAMMA-ECI or SI-G-ECI as well as checking if "sect1" is assigned with TOP SECRET.
        }else if( ((sect4 == "COMINT-GAMMA-ECI") || (sect4 == "SI-G-ECI")) && (sect1 == "TOP SECRET") ){
//this will check if the classification ORCON exists            
            if((orcon == "ORCON")||(orcon == "ORIGINATOR CONTROLLED")){
      
            status = 5;
       
//This will also check if TALENT KEYHOLE exists as well. If it does, the value of status will be increased by 3
            if((tk == "TALENT KEYHOLE") || (tk == "TK")){
    
            status += 3;
  
            
            }

            }

            

 //This else will execute if "sect2" was assigned with an invalid classification.           
        }else{

            status = 0;


        }

//This else will execute if "sect1" was assigned with UNCLASSIFIED.     
        }else{

            status = 0; 
          
        }

        return status;

            


    }

}

}
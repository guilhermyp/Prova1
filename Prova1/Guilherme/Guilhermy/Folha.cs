using System;

namespace Guilhermy;

public class Folha
{
    public int FolhaId { get; set; }
    public double Valor { get; set; }
    public int Quantidade { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public double SalarioBruto (double salariobruto){
        salariobruto = Valor * Quantidade;
        if(SalarioBruto <= 1903.98){
            return salariobruto;
        }else if(salariobruto>1903.99 && <= 2826.65){
            return salariobruto-(salariobruto*0,075);
        }
        {
            
        }
        

     }



}

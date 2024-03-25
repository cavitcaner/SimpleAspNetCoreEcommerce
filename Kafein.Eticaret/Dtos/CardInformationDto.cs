namespace Kafein.Eticaret.Dtos
{
    public class CardInformationDto {

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int Month;
        public int Year;
        public int CcvNumber { get; set; }

        //    MM/YYYY
        public string Expiration { get {
                return Month.ToString().PadLeft(2, '0') + "/" + Year.ToString();
            }
            set
            {
                Month = Convert.ToInt32(value.Split('/')[0]);
                Year = Convert.ToInt32(value.Split('/')[1]);
            }
        }
    }

}

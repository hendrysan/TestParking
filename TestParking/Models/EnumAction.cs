using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestParking.Models
{
   public enum EnumAction
    {
        create_parking_lot,
        park,
        leave,
        status,
        type_of_vehicles,
        registration_numbers_for_vehicles_with_ood_plate,
        registration_numbers_for_vehicles_with_event_plate,
        registration_numbers_for_vehicles_with_colour,
        slot_number_for_registration_number,
        slot_numbers_for_vehicles_with_colour,
        exit,
        clear,
        automation
        
    }
}

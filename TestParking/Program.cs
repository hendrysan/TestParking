using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParking.Models;

namespace TestParking
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isContinue = true;
            Program program = new Program();
            program.VehicleList = new List<Vehicles>();


            while (isContinue)
            {
                string _input = Console.ReadLine();


                var _splitInput = _input.Split(' ');

                if (_splitInput.Count() > 0)
                {

                    try
                    {
                        var enumCode = (EnumAction)System.Enum.Parse(typeof(EnumAction), _splitInput[0]);
                        var values = string.Join(" ", _splitInput.Skip(1).ToArray());

                        switch (enumCode)
                        {
                            case EnumAction.exit:
                                isContinue = false;
                                Console.WriteLine(string.Format("Thank You"));
                                Console.ReadLine();
                                break;
                            case EnumAction.clear:
                                Console.Clear();
                                break;
                            case EnumAction.create_parking_lot:
                                program.Max = Convert.ToInt32(values);
                                Console.WriteLine(string.Format("Created a parking lot with {0} slots", program.Max));
                                break;
                            case EnumAction.park:
                                var dataCurrent = program.VehicleList.Count;

                                if (program.Max <= 0)
                                    Console.WriteLine(string.Format("Max Lot not is {0}", program.VehicleList.Count));
                                else if (dataCurrent < program.Max)
                                {
                                    Vehicles model = null;
                                    var splitValue = values.Split(' ');

                                    if (splitValue.Count() == 3)
                                    {
                                        List<int> list = program.VehicleList.Select(x => x.ID).ToList();
                                        var missingID = Enumerable.Range(1, program.Max).Except(list).FirstOrDefault();

                                        if (missingID > 0)
                                        {
                                            model = new Vehicles()
                                            {
                                                ID = Convert.ToInt32(missingID),
                                                Number = splitValue[0],
                                                Color = splitValue[1],
                                                Type = splitValue[2],
                                            };
                                        }

                                    }


                                    if (model != null)
                                    {
                                        program.VehicleList.Add(model);

                                        program.VehicleList = program.VehicleList.OrderBy(x => x.ID).ToList();
                                        Console.WriteLine(string.Format("Allocated slot number : {0}", model.ID));
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format("Mapping input value invalid"));
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine(string.Format("Maximum slot is : {0}", program.Max));
                                    Console.WriteLine(string.Format("Sorry, parking lot is full"));
                                }
                                break;
                            case EnumAction.leave:
                                var dataExists = program.VehicleList.Where(x => x.ID.ToString() == values).FirstOrDefault();

                                if (dataExists == null)
                                    Console.WriteLine(string.Format("Slot {0} not found", values));
                                else
                                {
                                    program.VehicleList.Remove(dataExists);
                                    Console.WriteLine(string.Format("Slot number {0} is free", dataExists.ID));
                                }
                                break;
                            case EnumAction.status:
                                Console.WriteLine(string.Format("|   Slot   | No.                  | Type         | Color"));
                                if (program.VehicleList.Count > 0)
                                {
                                    foreach (var item in program.VehicleList)
                                    {
                                        Console.WriteLine(string.Format("|   {0}   | {1}                  | {2}         | {3}", item.ID, item.Number, item.Type, item.Color));
                                    }
                                }
                                else
                                    Console.WriteLine(string.Format("|   empty |"));
                                break;
                            case EnumAction.type_of_vehicles:
                                var count = program.VehicleList.Where(x => x.Type == values).Count();
                                Console.WriteLine(string.Format("{0}", count));
                                break;
                            case EnumAction.registration_numbers_for_vehicles_with_ood_plate: // nampilin tengah2nya aja, skip 1, dan terakhir
                                //var plateNumber = string.Join(", ", program.VehicleList.Select(x => x.Number).ToArray());
                                //Console.WriteLine(string.Format("{0}", plateNumber));
                                var dataFirst = program.VehicleList.First();
                                var dataLast = program.VehicleList.Last();
                                List<Vehicles> tmp = new List<Vehicles>();

                                foreach (var item in program.VehicleList)
                                {
                                    if ((item.ID != dataFirst.ID) && (item.ID != dataLast.ID))
                                        tmp.Add(item);
                                }

                                var plateNumber = string.Join(", ", tmp.Select(x => x.Number).ToArray());
                                Console.WriteLine(string.Format("{0}", plateNumber));
                                break;
                            case EnumAction.registration_numbers_for_vehicles_with_event_plate:
                                var _dataFirst = program.VehicleList.First();
                                var _dataLast = program.VehicleList.Last();
                                List<Vehicles> _tmp = new List<Vehicles>();

                                foreach (var item in program.VehicleList)
                                {
                                    if ((item.ID == _dataFirst.ID) || (item.ID == _dataLast.ID))
                                        _tmp.Add(item);
                                }

                                var _plateNumber = string.Join(", ", _tmp.Select(x => x.Number).ToArray());
                                Console.WriteLine(string.Format("{0}", _plateNumber));
                                break;

                            case EnumAction.registration_numbers_for_vehicles_with_colour:
                                var plateColor = string.Join(", ", program.VehicleList.Where(x => x.Color == values).Select(x => x.Number).ToArray());
                                Console.WriteLine(string.Format("{0}", plateColor));
                                break;

                            case EnumAction.slot_numbers_for_vehicles_with_colour:
                                var plateId = string.Join(", ", program.VehicleList.Where(x => x.Color == values).Select(x => x.ID).ToArray());
                                Console.WriteLine(string.Format("{0}", plateId));
                                break;
                            case EnumAction.slot_number_for_registration_number:
                                var data = program.VehicleList.Where(i => i.Number == values).FirstOrDefault();
                                if (data == null)
                                    Console.WriteLine(string.Format("Not found"));
                                else
                                    Console.WriteLine(string.Format("{0}", data.ID));
                                break;
                            //case EnumAction.automation:
                            //    Console.WriteLine("create_parking_lot 6");
                            //    break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine(string.Format("Command [{0}] not identified", _splitInput[0]));
                    }
                }

            }
        }
        public int Max { get; set; }

        public List<Vehicles> VehicleList { get; set; }

    }
}

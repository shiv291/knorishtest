using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer.Models;
using DatabaseLayer;

namespace Inventory.BusinessLib
{
    public class BoatOperation : IBoatOperation
    {
        public long SaveBoat(Boats obj)
        {
            try
            {                
                IDbRepository<Boats> ProdOperation = new DbRepository<Boats>();
                var result = ProdOperation.Save(obj);
                return result.BoatId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnregisterBoat(long Id)
        {
            try
            {
                IDbRepository<Boats> ProdOperation = new DbRepository<Boats>();
                var result=ProdOperation.Filter(d => d.BoatId == Id).FirstOrDefault();
                if (result != null)
                {
                    result.IsActive = false;
                    ProdOperation.Update(result);
                }
                else
                {
                    throw new Exception("Invalid Id");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReturnBoat(long boatId)
        {
            try
            {
                IDbRepository<RentBoatToCustomer> ProdOperation = new DbRepository<RentBoatToCustomer>();
                var result = ProdOperation.Filter(d => d.BoatId == boatId && d.IsReturn==false).FirstOrDefault();
                if (result != null)
                {
                    result.IsReturn = true;
                    ProdOperation.Update(result);
                }
                else
                {
                    throw new Exception("Invalid Id");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RentBoatToCustomer RentToCUstomer(RentBoatToCustomer obj)
        {
            try
            {
                IDbRepository<RentBoatToCustomer> ProdOperation = new DbRepository<RentBoatToCustomer>();
                var data=ProdOperation.Save(obj);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public interface IBoatOperation
    {
        long SaveBoat(Boats obj);
        void UnregisterBoat(long Id);
        void ReturnBoat(long boatId);
        RentBoatToCustomer RentToCUstomer(RentBoatToCustomer obj);

    }
}

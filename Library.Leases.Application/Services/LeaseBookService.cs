using Library.Core;
using Library.Items.Services.Services;
using Library.Leases.Application.Dtos;
using Library.Leases.Domain.Exceptions;
using Library.Leases.Domain.Stores;

namespace Library.Leases.Application.Services
{
    public class LeaseBookService
    {
        private readonly ItemService _itemService;
        private readonly IReaderStore _readerStore;

        public LeaseBookService(IReaderStore readerStore, ItemService itemService)
        {
            _itemService = itemService;
            _readerStore = readerStore;
        }

        public OperationStatus LeaseBook(NewLeaseDto leaseDto)
        {
            try
            {
                if (_itemService.GetAvailableItemsQty(leaseDto.BookId) > 0)
                {
                    var reader = _readerStore.GetReaderById(leaseDto.ReaderId);
                    reader.LeaseBook(leaseDto.BookId);

                    _itemService.DecreaseItemAvailability(leaseDto.BookId);

                    _readerStore.SaveReader(reader);

                    return OperationStatus.CompletedSuccessfully;
                }

                return new OperationStatus()
                {
                    Success = false,
                    ErrorMessage = "Requested book is not available"
                };
            }
            catch (MaxConcurrentLeasesExceeded e)
            {
                return new OperationStatus()
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}

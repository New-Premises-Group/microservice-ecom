﻿using IW.Common;
using IW.Models.DTOs;
using IW.Models.DTOs.Inventory;
using IW.Models.DTOs.InventoryDto;

namespace IW.Interfaces
{
    public interface IInventoryService
    {
        Task CreateInventory(CreateInventory input);
        Task<InventoryDto> GetInventory(int id);
        Task<IEnumerable<InventoryDto>> GetInventories(int offset, int amount);
        Task<IEnumerable<InventoryDto>> GetInventories(GetInvenory query, int offset , int amount );
        Task UpdateInventory(int id, UpdateInventory model);
        Task UpdateInventories(ICollection<InventoryDto> inventories);
        Task UpdateStocks(ICollection<InventoryDto> inventoryDto,TRANSACTION_TYPE type);
        Task DeleteInventory(int id);
    }
}

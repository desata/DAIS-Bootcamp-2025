﻿using Exam.Services.DTOs.Favorite;

namespace Exam.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<FavoriteInfo> GetByIdAsync(int favoriteId);
        Task<List<FavoriteInfo>> GetAllByUserIdAsync(int userId);
        Task<FavoriteInfo> GetByUserIdAsync(int userId);
        Task<CreateFavoriteResponse> CreateFavoriteAsync(CreateFavoriteRequest request);
        Task<DeleteFavoriteResponse> DeleteFavoriteAsync(DeleteFavoriteRequest request);
    }
}

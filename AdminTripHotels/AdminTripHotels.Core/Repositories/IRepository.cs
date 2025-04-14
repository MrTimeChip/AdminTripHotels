using System.Linq.Expressions;

namespace AdminTripHotels.Core.Repositories;

/// <summary>
///     Репозиторий сущности
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     Получение всех сущностей из хранилища
    /// </summary>
    /// <returns>Все сущности в хранилище</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    ///     Добавление сущности в хранилище
    /// </summary>
    /// <param name="item">Сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность после добавления</returns>
    Task AddAsync(TEntity item, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Добавление указанных сущностей
    /// </summary>
    /// <param name="entities">Сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Обновление сущности
    /// </summary>
    /// <param name="item">Сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Удаление сущности
    /// </summary>
    /// <param name="item">Сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAsync(TEntity item, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Удаление указанных сущностей
    /// </summary>
    /// <param name="items">Сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAllAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Удаление сущностей по условию
    /// </summary>
    /// <param name="predicate">Условие выборки сущностей для удаления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
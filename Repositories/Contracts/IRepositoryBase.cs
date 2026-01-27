using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories.Contracts;

public interface IRepositoryBase<T>
{

    // CRUD OPR.

    /*
     * IQueryable<T>: Sorgu hemen çalışmaz, üzerine .Where()/.Take() eklenebilir.
     * Son anda (.ToList()) tüm filtreler tek SQL'e dönüşür → Sadece lazım olan veri gelir.
     *
     * trackChanges:
     * - true: EF Core değişiklikleri takip eder (Update/Delete için).
     * - false: Takip kapalı, sadece okuma yapılır (daha hızlı).
     *
     * List<T> vs IQueryable<T>:
     * - List: Anında tüm veri RAM'e çekilir (1M satır = felaket).
     * - IQueryable: Filtreleme DB'de yapılır, sadece sonuç gelir (hızlı).
     */
    // Get all entities, optionally with change tracking disabled.
    IQueryable<T> FindAll(bool trackChanges);

    /*
     * Expression<Func<T, bool>>: Lambda sorgularını (x => x.Id == 5) temsil eder.
     *
     * Neden Expression?
     * - Func: C#'ta çalışır. SQL'e çevrilemez → Tüm veri RAM'e çekilir (YAVAŞ).
     * - Expression: EF Core tarafından incelenir → SQL'e çevrilir (HIZLI).
     *
     * Örnek: x => x.Id == 5  →  WHERE Id = 5
     *
     * Özet: Expression = Lambda'nın SQL'e tercüme edilebilir haritası.
     */

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    /*
             Queryable: Dönüş tipi. Sorgu hemen çalışmaz, üzerine filtre eklenebilir. Son anda (.ToList()) tek SQL olarak çalışır.

       T: Generic tip. Drone, Book, User ne olursa o. Hangi tablo üzerinde çalışıyorsan o tipi alır.

       Func<T, bool>: Fonksiyon şablonu. "Bana T ver, sana true/false döneyim" demek. Örnek: d => d.Battery < 20 → Pil 20'den küçükse true döner.

       Expression<...>: Lambda'yı kod olarak derlemez, veri yapısı (harita) olarak tutar. EF Core bu haritayı okur, SQL'e çevirir. d => d.Battery < 20 → WHERE Battery < 20 olur.

       Neden Func değil Expression?: Func kullanılsa C# kodu olur, EF Core çeviremez, tüm veri RAM'e çekilir. Expression ile harita tutulur, EF Core SQL üretir, filtreleme DB'de olur.

       trackChanges: false = Takip kapalı, sadece okuma, hızlı. true = Takip açık, güncelleme yapılacaksa.
 */

    // Add a new entity.
    void Create(T entity);
    // Update an existing entity.
    void Update(T entity);
    // Delete an entity.
    void Delete(T entity);

}
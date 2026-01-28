namespace Repositories.Contracts;

/*
 * ═══════════════════════════════════════════════════════════════════════════
 * IRepositoryManager - DELEGATION PATTERN / DELEGASyON DESENİ
 * ═══════════════════════════════════════════════════════════════════════════
 *
 * PURPOSE / AMAÇ:
 * EN: Acts as a WAITER - provides access to all kitchen stations (repositories)
 *     and rings the bell (Save) to finalize all orders in one transaction.
 * TR: GARSON gibi çalışır - tüm mutfak istasyonlarına (repositories) erişim sağlar
 *     ve tüm siparişleri tek işlemde sonlandırmak için zili çalar (Save).
 *
 * WHY NO CRUD INHERITANCE / NEDEN CRUD MİRASI YOK:
 * EN: Waiters don't cook! They direct customers to chefs who do the actual work.
 * TR: Garsonlar yemek pişirmez! Müşterileri asıl işi yapan şeflere yönlendirirler.
 *
 * ───────────────────────────────────────────────────────────────────────────
 * CODE LINE EXPLANATION / KOD SATIRI AÇIKLAMASI:
 * ───────────────────────────────────────────────────────────────────────────
 */
public interface IRepositoryManager
{
    // LINE / SATIR: IDroneRepository Drone { get; }
    // ───────────────────────────────────────────────────────────────────────
    // EN: Property that returns access to the Drone kitchen station.
    //     Like asking waiter: "Where's the pizza chef?" → Waiter points to pizza station
    //     WAITER'S MENU: "For drone orders, go to the Drone kitchen (DroneRepository)"
    //
    // TR: Drone mutfak istasyonuna erişim döndüren özellik.
    //     Garsona sormak gibi: "Pizza şefi nerede?" → Garson pizza istasyonunu gösterir
    //     GARSONUN MENÜSÜ: "Drone siparişleri için Drone mutfağına gidin (DroneRepository)"
    // ───────────────────────────────────────────────────────────────────────

    IDroneRepository Drone { get; }


    // LINE / SATIR: void Save();
    // ───────────────────────────────────────────────────────────────────────
    // EN: Finalizes ALL repository changes in ONE database transaction.
    //     Like waiter ringing the SERVICE BELL: "All orders ready! Serve together!"
    //     If pizza burns, cancel the burger too (transaction safety)
    //
    // TR: TÜM repository değişikliklerini TEK veritabanı işleminde tamamlar.
    //     Garsonun SERVİS ZİLİNİ çalması gibi: "Tüm siparişler hazır! Birlikte servise!"
    //     Pizza yanmışsa, burger'ı da iptal et (işlem güvenliği)
    // ───────────────────────────────────────────────────────────────────────

    void Save();
}

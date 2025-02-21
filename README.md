# Car Sales Application / Araba İlanı Uygulaması

## 1. Technologies / Teknolojiler
<img src="https://github.com/user-attachments/assets/7391f85b-7d84-40bf-ad23-15b79a114b85" height="350px">
<br/><br/>
<table border="1">
  <thead>
    <tr>
      <th>Technology</th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Keycloak & JWT</td>
      <td>Centralized authentication and authorization.</td>
      <td>Merkezi kimlik doğrulama ve yetkilendirme.</td>
    </tr>
    <tr>
      <td>Redis</td>
      <td>Cache brand, model, and car listings.</td>
      <td>Marka, model ve araba ilanları cachelenir.</td>
    </tr>
    <tr>
      <td>Serilog</td>
      <td>Errors and operations in the app are logged in JSON format.</td>
      <td>Uygulamadaki hatalar ve işlemler JSON formatında kaydedilir.</td>
    </tr>
    <tr>
      <td>MySQL</td>
      <td>All data is stored relationally.</td>
      <td>Tüm veriler ilişkisel olarak saklanır.</td>
    </tr>
    <tr>
      <td>ElasticSearch</td>
      <td>Used for full-text search in listings.</td>
      <td>İlanlarda arama yapmak için full-text search amaçlı kullanılmıştır.</td>
    </tr>
  </tbody>
</table>

## 2. Authentication and Authorization / Giriş ve Yetkilendirme
<img src="https://github.com/user-attachments/assets/40710911-c149-4177-a899-05281f20c45c" height="350px">
<br/><br/>
* There are roles of authorization; Admin and User. / İki tip yetkilendirme mevcuttur; Yönetici ve Kullanıcı.
<table border="1">
  <thead>
    <tr>
      <th>Function</th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>SignIn(e,p)</td>
      <td>Tries to get an access token through Keycloak based on email and password.</td>
      <td>E-posta ve şifreye göre Keycloak üzerinden access token almaya çalışır.</td>
    </tr>
    <tr>
      <td>SignUp</td>
      <td>First admin signs in, creates a new user using the admin token, and then the new user signs in with their credentials. <br/><br/>SignIn(Admin) -> CreateUser(e,p) -> SignIn(e,p) => AccessToken</td>
      <td>Önce admin SignIn olur, Admin Token'ı kullanılarak sisteme yeni kullanıcı eklenir. En son yeni kullanıcı bilgileriyle tekrar SignIn yapılır. <br/><br/>SignIn(Admin) -> CreateUser(e,p) -> SignIn(e,p) => AccessToken</td>
    </tr>
  </tbody>
</table>

## 3. Layered Architecture
<table>
        <thead>
            <tr>
                <th>Layer</th>
                <th></th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>API (Presentation Layer)</td>
                <td>Manages user interaction. Handles API requests and returns the response.</td>
                <td>Kullanıcı ile etkileşimi sağlar. API isteklerini alır ve sonucu döner.</td>
            </tr>
            <tr>
                <td>DAL (Data Access Layer)</td>
                <td>Interacts with the database. Handles data retrieval, updating, and deletion.</td>
                <td>Veritabanı ile iletişim kurar. Verileri alır, günceller veya siler.</td>
            </tr>
            <tr>
                <td>BLL (Business Logic Layer)</td>
                <td>Contains business rules, logic, logging, and caching. Processes data from API and passes it to the database.</td>
                <td>İş kurallarını, mantığı, loglama ve cachelemeyi içerir. API'den gelen verileri işler ve veritabanına iletir.</td>
            </tr>
            <tr>
                <td>Core Layer</td>
                <td>Contains core functionality, helper classes, and models.</td>
                <td>Çekirdek işlevsellik, yardımcı sınıflar ve modelleri barındırır.</td>
            </tr>
        </tbody>
    </table>

## 4. Entity Diagram
<img src="https://github.com/user-attachments/assets/f2a42e55-32e5-43dc-8200-48c13461d1f1" height="350px">
<table border="1">
  <thead>
    <tr>
      <th>Entity Adı</th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>User</td>
      <td>The owner of the car listing. The person who manages and shares the car details, including photos.</td>
      <td>İlan sahibidir. Arabayla ilgili tüm bilgileri yöneten ve fotoğrafları yükleyen kişidir.</td>
    </tr>
    <tr>
      <td>Brand</td>
      <td>The brand or manufacturer of the car. It defines the maker of the vehicle.</td>
      <td>Arabayı üreten marka veya üreticidir. Arabanın ait olduğu firmayı tanımlar.</td>
    </tr>
    <tr>
      <td>Model</td>
      <td>The specific model of the car under a given brand.</td>
      <td>Marka altında bulunan araba modelini belirtir.</td>
    </tr>
    <tr>
      <td>Photo</td>
      <td>Photos of the car. They visually represent the car in the listing.</td>
      <td>Arabaya ait fotoğraflardır. İlanın görsel temsili sağlanır.</td>
    </tr>
    <tr>
      <td>CarPart</td>
      <td>Parts of the car. These parts can be new, changed, repainted, or original.</td>
      <td>Arabaya ait parçalardır. Bu parçalar değişmiş, boyalı, orijinal olabilir.</td>
    </tr>
  </tbody>
</table>



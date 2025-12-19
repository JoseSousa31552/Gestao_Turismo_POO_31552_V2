/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Teste unitários para a aplicação Turismo_V1.
*/
using Moq;
using System.IO;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository;
using Turismo_V1.Models.Repository.Interfaces;
using Turismo_V1.ValidationsRegister;
using Turismo_V1.ViewModels.Services;

namespace Turismo_V1.Tests
{
    public class Tests
    {
        [TestFixture]

        /// <summary>
        /// Testes unitários para a Dll RegisterValidator.
        /// </summary>
        public class RegisterValidationTests
        {
            // --- Test Phone ---
            [Test]
            public void IsValidPhoneNumber_Valid9Digits_ReturnsTrue()
            {
                // Act
                bool result = RegisterValidator.IsValidPhoneNumber("912345678");

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void IsValidPhoneNumber_WithLetters_ReturnsFalse()
            {
                // Act
                bool result = RegisterValidator.IsValidPhoneNumber("912abc678");

                // Assert
                Assert.IsFalse(result, "Deve falhar se tiver letras");
            }

            [Test]
            public void IsValidPhoneNumber_WrongLength_ReturnsFalse()
            {
                // Act
                bool result = RegisterValidator.IsValidPhoneNumber("12345");

                // Assert
                Assert.IsFalse(result);
            }

            // --- Test EMAIL ---
            [Test]
            public void IsValidEmai_ReturnsTrue()
            {
                // Act
                bool result = RegisterValidator.IsValidEmail("teste@ipca.pt");
                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void IsValidEmail_NoAtSign_ReturnsFalse()
            {
                // Act
                bool result = RegisterValidator.IsValidEmail("teste.ipca.pt");
                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void IsValidEmail_Empty_ReturnsFalse()
            {
                // Act
                bool result = RegisterValidator.IsValidEmail("");
                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void IsValidEmail_NoAtPoint_ReturnsFalse()
            {
                // Act
                bool result = RegisterValidator.IsValidEmail("teste@ipca");
                // Assert
                Assert.IsFalse(result);
            }
        }

        /// <summary>
        /// Testes unitários para o service DeleteReservation.
        /// </summary>
        public class DeleteReservationTests
        {
            private Mock<IBookingRepository> _mockBookingRepo;
            private Mock<ICheckInRepository> _mockCheckInRepo;
            private DeleteReservation _service;

            private Register _dummyClient;
            private HomeLodging _dummyLodging;

            [SetUp]
            public void Setup()
            {
                _mockBookingRepo = new Mock<IBookingRepository>();
                _mockCheckInRepo = new Mock<ICheckInRepository>();
                _service = new DeleteReservation(_mockBookingRepo.Object, _mockCheckInRepo.Object);

                _dummyClient = new Register("User", "Pass", "Nome", "email", "911111111");
                _dummyLodging = new HomeLodging("Casa", "Morada", 100, DateTime.Now, DateTime.Now.AddDays(10));
            }

            [Test]
            public void CancelBooking_NullBooking_ReturnsFalse()
            {
                // Act
                bool result = _service.CancelBooking(null);

                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void CancelBooking_HasCheckIn_ReturnsFalse()
            {
                // Arrange
                Guid idDaReserva = Guid.NewGuid();

                
                var booking = new Booking(_dummyClient, _dummyLodging, DateTime.Now, DateTime.Now)
                {
                    ID = idDaReserva
                };

               
                var listaCheckIns = new List<CheckIN>
                {
                new CheckIN(booking, DateTime.Now)
                };

                _mockCheckInRepo.Setup(repo => repo.GetAll()).Returns(listaCheckIns);

                // Act
                bool result = _service.CancelBooking(booking);

                // Assert
                Assert.IsFalse(result, "Não deve cancelar se houver check-in.");
            }

            [Test]
            public void CancelBooking_NoCheckIn_ReturnsTrue()
            {
                // Arrange
                var booking = new Booking(_dummyClient, _dummyLodging, DateTime.Now, DateTime.Now)
                {
                    ID = Guid.NewGuid()
                };

                _mockCheckInRepo.Setup(repo => repo.GetAll()).Returns(new List<CheckIN>());

                // Act
                bool result = _service.CancelBooking(booking);

                // Assert
                Assert.IsTrue(result, "Deve cancelar com sucesso se não houver check-in.");
            }
        }

        /// <summary>
        /// Testes unitários para o BookingRepository.
        /// </summary>
        public class BookingRepositoryTests
        {
            private BookingRepository _repository;
            private Register _dummyClient;
            private HomeLodging _dummyLodging;

            [SetUp]
            public void Setup()
            {
                _repository = new BookingRepository();

                _dummyClient = new Register( "Nome Teste","email@test.com","911111111","UserTest", "pass"    )          
                {
                    ID = Guid.NewGuid()
                };

                _dummyLodging = new HomeLodging("Casa Teste", "Rua Teste", 100.0, new DateTime(2026, 1, 1), new DateTime(2026, 12, 31))
                {
                    ID = Guid.NewGuid()
                };
            }

           
            [Test]
            public void CreateBooking_ValidDates_ReturnsTrue()
            {
                // Arrange 
                DateTime inDate = new DateTime(2026, 3, 10); 
                DateTime outDate = new DateTime(2026, 3, 15);

                // Act
                bool result = _repository.CreateBooking(_dummyClient, _dummyLodging, inDate, outDate);

                // Assert
                Assert.IsTrue(result, "Devia criar reserva com datas válidas no futuro");
            }

            
            [Test]
            public void CreateBooking_CheckOutBeforeCheckIn_ReturnsFalse()
            {
                // Arrange 
                DateTime inDate = new DateTime(2026, 4, 10);
                DateTime outDate = new DateTime(2026, 4, 09);

                // Act
                bool result = _repository.CreateBooking(_dummyClient, _dummyLodging, inDate, outDate);

                // Assert
                Assert.IsFalse(result, "Não pode permitir CheckOut antes do CheckIn");
            }

          
            [Test]
            public void CreateBooking_OverlapDates_ReturnsFalse()
            {
                // Arrange
                // Reserva A: 10 a 20 de Agosto 2026
                var in1 = new DateTime(2026, 8, 10);
                var out1 = new DateTime(2026, 8, 20);
                _repository.CreateBooking(_dummyClient, _dummyLodging, in1, out1);

                // Reserva B: 15 a 18 de Agosto 2026 (erro)
                var in2 = new DateTime(2026, 8, 15);
                var out2 = new DateTime(2026, 8, 18);

                // Act
                bool result = _repository.CreateBooking(_dummyClient, _dummyLodging, in2, out2);

                // Assert
                Assert.IsFalse(result, "O algoritmo falhou: permitiu duas reservas ao mesmo tempo!");
            }

            
            [Test]
            public void GetBookingsByClient_ReturnsOnlyUserBookings()
            {
                // Arrange 
                _repository.CreateBooking(_dummyClient, _dummyLodging, new DateTime(2026, 9, 1), new DateTime(2026, 9, 5));

                // Act
                List<Booking> minhasReservas = _repository.GetBookingsByClient(_dummyClient);

                // Assert
                Assert.IsNotEmpty(minhasReservas);
                Assert.AreEqual(_dummyClient.ID, minhasReservas[0].Client.ID);
            }
        }

        /// <summary>
        /// Testes unitários para o CheckInRepository.
        /// </summary>
        public class CheckInRepositoryTests
        {
            private CheckInRepository _repository;
            private Register _dummyClient;
            private HomeLodging _dummyLodging;
            private Booking _bookingValida;

            [SetUp]
            public void Setup()
            {
                _repository = new CheckInRepository();

                _dummyClient = new Register("Nome", "email", "911", "user", "pass")
                {
                    ID = Guid.NewGuid()
                };

                _dummyLodging = new HomeLodging("Casa", "Rua", 100, new DateTime(2026, 1, 1), new DateTime(2026, 12, 31))
                {
                    ID = Guid.NewGuid()
                };

              
                _bookingValida = new Booking(_dummyClient, _dummyLodging, new DateTime(2026, 5, 10),  new DateTime(2026, 5, 20)   )
                {
                    ID = Guid.NewGuid()
                };
            }

            [Test]
            public void CreateCheckIn_ValidDate_ReturnsTrue()
            {
                // Arrange
                DateTime dataCheckIn = new DateTime(2026, 5, 15);

                // Act
                bool result = _repository.CreateCheckIn(_bookingValida, dataCheckIn);

                // Assert
                Assert.IsTrue(result, "Devia aceitar CheckIn dentro das datas da reserva.");
            }

            [Test]
            public void CreateCheckIn_DateBeforeBooking_ReturnsFalse()
            {
                // Arrange
                DateTime dataCedoDemais = new DateTime(2026, 5, 9);

                // Act
                bool result = _repository.CreateCheckIn(_bookingValida, dataCedoDemais);

                // Assert
                Assert.IsFalse(result, "Não devia aceitar CheckIn antes da data de entrada prevista.");
            }

            [Test]
            public void CreateCheckIn_DateAfterBooking_ReturnsFalse()
            {
                // Arrange
                DateTime dataTardeDemais = new DateTime(2026, 5, 21);

                // Act
                bool result = _repository.CreateCheckIn(_bookingValida, dataTardeDemais);

                // Assert
                Assert.IsFalse(result, "Não devia aceitar CheckIn depois da data de saída prevista.");
            }

            [Test]
            public void CreateCheckIn_Duplicate_ReturnsFalse()
            {
                // Arrange 
                DateTime dataValida = new DateTime(2026, 5, 12);

                
                _repository.CreateCheckIn(_bookingValida, dataValida);

                // Act 
                bool result = _repository.CreateCheckIn(_bookingValida, dataValida);

                // Assert
                Assert.IsFalse(result, "Não devia permitir fazer CheckIn duas vezes na mesma reserva.");
            }

            [Test]
            public void GetCheckINByClient_ReturnsCorrectList()
            {
                // Arrange
                _repository.CreateCheckIn(_bookingValida, new DateTime(2026, 5, 10));

                // Act
                var lista = _repository.GetCheckINByClient(_dummyClient);

                // Assert
                Assert.IsNotEmpty(lista);
                Assert.AreEqual(_dummyClient.ID, lista[0].BookingInfo.Client.ID);
            }
        }

        /// <summary>
        /// Testes unitários para o RegisterRepository.
        /// </summary>
        public class RegisterRepositoryTests
        {
            private RegisterRepository _repository;

            [SetUp]
            public void Setup()
            {
                if (File.Exists("users.json"))
                {
                    File.Delete("users.json");
                }

                _repository = new RegisterRepository();
            }

            [Test]
            public void CreateUser_NewUser_ReturnsTrue()
            {
                // Act
                string userUnico = "User_" + Guid.NewGuid().ToString();
                bool result = _repository.CreateUser(userUnico, "password123", "email@test.com", "912345678", "Nome Teste");

                // Assert
                Assert.IsTrue(result, "Devia criar o utilizador porque é novo e não existe na lista.");
            }
          
            [Test]
            public void CreateUser_DuplicateUsername_ReturnsFalse()
            {
                // Arrange
                _repository.CreateUser("userDuplicado", "pass1", "email1@test.com", "911", "Nome1");

                // Act
                bool result = _repository.CreateUser("userDuplicado", "pass2", "email2@test.com", "922", "Nome2");

                // Assert
                Assert.IsFalse(result, "O repositório falhou: permitiu criar dois utilizadores com o mesmo Username!");
            }

           
            [Test]
            public void GetUserByUsername_UserExists_ReturnsUser()
            {
                // Arrange
                string targetUsername = "omaior";
                _repository.CreateUser(targetUsername, "pass", "email", "999", "O Maior");

                // Act
                Register? userEncontrado = _repository.GetUserByUsername(targetUsername);

                // Assert
                Assert.IsNotNull(userEncontrado);
                Assert.AreEqual(targetUsername, userEncontrado.User);
                Assert.AreEqual("O Maior", userEncontrado.Name);
            }

            
            [Test]
            public void GetUserByUsername_UserDoesNotExist_ReturnsNull()
            {
                // Arrange
                _repository.CreateUser("outroUser", "pass", "email", "111", "Outro");

                // Act
                Register? resultado = _repository.GetUserByUsername("fantasma");

                // Assert
                Assert.IsNull(resultado, "Devia retornar null se o username não existe.");
            }

            
            [Test]
            public void GetUserByUsername_EmptyString_ReturnsNull()
            {
             
                // Act
                Register? resultado = _repository.GetUserByUsername("");

                // Assert
                Assert.IsNull(resultado, "Devia retornar null se passarmos uma string vazia.");
            }
        }


        /// <summary>
        /// Testes unitários para o HomeLodgingRepository.
        /// </summary>
        public class HomeLodgingRepositoryTests
        {
            private HomeLodgingRepository _repository;

            [SetUp]
            public void Setup()
            {
                if (File.Exists("HomeLodging.json"))
                {
                   File.Delete("HomeLodging.json");
                }
                _repository = new HomeLodgingRepository();
            }

            [Test]
            public void CreateHomeLodging_NewLodging_ReturnsTrue()
            {
                // Arrange
                string nome = "Casa da Praia" + Guid.NewGuid().ToString();
                DateTime inicio = new DateTime(2026, 6, 1);
                DateTime fim = new DateTime(2026, 9, 30);

                // Act
                bool result = _repository.CreateHomeLodging(nome, "Rua do Mar", 150.0, inicio, fim);

                // Assert
                Assert.IsTrue(result, "Devia criar o alojamento com sucesso.");

            
            }

   
            [Test]
            public void CreateHomeLodging_DuplicateName_ReturnsFalse()
            {
                // Arrange
                _repository.CreateHomeLodging("Casa Teste", "Rua A", 100, DateTime.Now, DateTime.Now.AddDays(10));

                // Act
                bool result = _repository.CreateHomeLodging("Casa Teste", "Rua B", 200, DateTime.Now, DateTime.Now.AddDays(10));

                // Assert
                Assert.IsFalse(result, "O repositório falhou: permitiu criar dois alojamentos com o mesmo Nome!");
            }

        
            [Test]
            public void GetHomeLodgingByName_Exists_ReturnsLodging()
            {
                // Arrange
                string targetName = "Solar do Minho";
                _repository.CreateHomeLodging(targetName, "Viana", 80, DateTime.Now, DateTime.Now.AddDays(5));

                // Act
                HomeLodging? resultado = _repository.GetHomeLodgingByname(targetName);

                // Assert
                Assert.IsNotNull(resultado);
                Assert.AreEqual(targetName, resultado.NameHomeLodging);
            }

          
            [Test]
            public void GetHomeLodgingByName_DoesNotExist_ReturnsNull()
            {
                // Arrange
                _repository.CreateHomeLodging("Outra Casa", "Rua X", 50, DateTime.Now, DateTime.Now.AddDays(1));

                // Act
                HomeLodging? resultado = _repository.GetHomeLodgingByname("Casa Fantasma");

                // Assert
                Assert.IsNull(resultado, "Devia retornar null se o alojamento não existe.");
            }

      
            [Test]
            public void GetHomeLodgingByName_EmptyString_ReturnsNull()
            {
             
                // Act
                HomeLodging? resultado = _repository.GetHomeLodgingByname("");

                // Assert
                Assert.IsNull(resultado, "Devia retornar null para string vazia.");
            }
        }






       










        








































    }
 }

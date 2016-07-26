using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using WebMoney.Cryptography;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Configuration;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Responses;
using WebMoney.XmlInterfaces.Sandbox.Properties;

namespace WebMoney.XmlInterfaces.Sandbox
{
    class Program
    {
        private readonly WmId _primaryWmId = (WmId)000000000000;

        //private readonly KeeperKey _primaryKeeperKey = new KeeperKey("");
        //private readonly WmId _secondaryWmId = (WmId)000000000000;
        ////private readonly Purse _primaryPurse = Purse.Parse(""); // кошелек с деньгами
        //private readonly Purse _primaryPurse = Purse.Parse(""); // кошелек с деньгами
        //private readonly Purse _secondaryPurse = Purse.Parse("");

        private readonly Purse _storePurse = Purse.Parse("U259441224405");

        //private readonly Description _clientFirstName = (Description)"Иван";
        //private readonly Description _clientSecondName = (Description)"Иванов";
        //private readonly Phone _clientPhone = Phone.Parse("");
        //private readonly BankCard _clientBankCard = BankCard.Parse("");
        
        // Корневой сертификат WebMoney: годен до марта 2035
        private readonly X509Certificate2 _webMoneyRootCertificate =
            new X509Certificate2(
                Convert.FromBase64String(
                    "MIIFsTCCA5mgAwIBAgIQA7dHzSZ7uJdBxFycIWn+WjANBgkqhkiG9w0BAQUFADBrMSswKQYDVQQLEyJXTSBUcmFuc2ZlciBDZXJ0aWZpY2F0aW9uIFNlcnZpY2VzMRgwFgYDVQQKEw9XTSBUcmFuc2ZlciBMdGQxIjAgBgNVBAMTGVdlYk1vbmV5IFRyYW5zZmVyIFJvb3QgQ0EwHhcNMTAwMzEwMTczNDU2WhcNMzUwMzEwMTc0NDUxWjBrMSswKQYDVQQLEyJXTSBUcmFuc2ZlciBDZXJ0aWZpY2F0aW9uIFNlcnZpY2VzMRgwFgYDVQQKEw9XTSBUcmFuc2ZlciBMdGQxIjAgBgNVBAMTGVdlYk1vbmV5IFRyYW5zZmVyIFJvb3QgQ0EwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQDFLJXtzEkZxLj1HIj9EhGvajFJ7RCHzF+MK2ZrAgxmmOafiFP6QD/aVjIexBqRb8SVy38xH+wthqkZgLMOGn8uDNpFieEMoX3ZRfqtCcD76KDySTOX1QUwHAzBfGuhe2ZQULUIjxdPRa4NDyvmXh4pE/s1+/7dGbUZs/JpYYaD2xxAt5PDTjylsKOk4FMb5kv6jzORkXku5UKFGUXEXbbf1xzgYHMIzoeJGn+iPgVFYAvkkQyvxEaVj0lNE+q/ua761krgCo47BiH1zMFzkv4uNHEZfe/lyHaozzbsu6yaK3EdrURSLuWrlxKy9yo3xDe9TPkzkhPeJPbV7YgvUUtWSeAJpksBU8GCALEhSgXOfHckuJFj9QB3YecHBvjdSiAUuntwM/iHvtSOXEUHxqW75E2Gq/2L4vBcxArXVdbUrVQDF3klzYu17OFgfe1hHHMHzgr4HBMLZiRCcvNLqghBCVxu1DM15YDfw+wnNV/5dUPx60tiocmCZpJKTwVl8gc85QCPyREujey8F0kgdgssQosPWTTWDg7X4Ifw20VkplHZDr29K5HdwLe56TvOI/4H24XJdqpAxoLBx9PL6ZXxH52wU0bSluL8/joXGzavFrhsXH7jJocH6tsFVzBZrmnVswbUMHDNL3xSnr5fAAXXZa7UwHd3pq/fsdG7s9PByQIDAQABo1EwTzALBgNVHQ8EBAMCAYYwDwYDVR0TAQH/BAUwAwEB/zAdBgNVHQ4EFgQUsTCnSwOZT4Q2HBN9V/TrafuIG8MwEAYJKwYBBAGCNxUBBAMCAQAwDQYJKoZIhvcNAQEFBQADggIBAAy5jHDFpVWtF209N30I+LHsiqMaLUmYDeV6sUBJqmWAZav7pWnigiMLuJd9yRa/ow6yKlKPRi3sbKaBwsAQ+xnz811nLFBBdS4PkwlHu1B7P4B2YbcqmF6k1QieJBZxOn7wledtnoBAkZ4d6HEW1OM5cvCoyj8YAdJTZIBzn61aNt/viPvypIUQf6Ps6Q2daNEAj7DoxIY8crnOaSIGdGmlT/y/edSqWv9Am5e9KXkJhQWMnGXh43wJYyHTetxVWPS43bW7gIUADYycKSH3isrBN5xQOFXMfL+lVHHSs7ap23DOo7xIDenm5PWz+QdDDFz3zLVeRovnkIdka/Wgk3f6rFfKB0y5POJ+BJvkorIYNZiN3dnmc6cDP840BUMv3BUrOe8iSy5lRr8mR+daktbZfO8E/rAb3zEdN+KG/CNJfAnQvp6DT4LqY/J9pG+VusH5GpUwuXr7UqLwEnd1LRp7qm28Cic7fegUnnUpkuF4ZFq8pWq8w59sOWlRuKBuWX46OghMrjgD0AN1hlA2/d5ULImX70Q2te3xiS1vrQhu77mkb/jA4/9+YfeT7VMpbnC3OoHiZ2bjudKnthlOs+AuUvzB4Tqo62VSF5+r0sYI593S+STmaZBAzsoaoEB7qxqKbEKCvXb9BlXkL76xIOEkbSIdPIkGXM4aMo4mTVz7"));

        private uint _orderId;
        private uint _tranId;

        static void Main(string[] args)
        {
            var program = new Program();

            var initializer = new Initializer(WmId.Parse("301095414760"), "fzRsA9VpOno1GANj0JrkQQjB3CuOuwwDH13dldPs");
            initializer.Apply();

            // XML-конфигурация
            //var configurator = new Configurator();
            //configurator.Apply();

            Settings.Default.OrderId++;
            Settings.Default.TranId++;
            Settings.Default.Save();

            program._orderId = Settings.Default.OrderId;
            program._tranId = Settings.Default.TranId;
            
            CertificateValidator.RegisterTrustedCertificate(program._webMoneyRootCertificate);

            //// X1. Выписка счета
            //// X4. История выписанных счетов (поиск по номеру счета)
            //// Отмена счета (недокументированный интерфейс)
            //try
            //{
            //    program.OriginalInvoiceTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X2.Перевод средств
            //// X3.История операций
            //try
            //{
            //    program.OriginalTransferTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X5. Завершение операции с протекцией
            //try
            //{
            //    program.ProtectionFinisherTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X6. Отправка сообщения
            //try
            //{
            //    program.MessageTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X7. Проверка подписи
            //try
            //{
            //    program.SignatureInspectorTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X8. Поиск по идентификатору или кошельку
            //try
            //{
            //    program.WmIdFinderTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X9. Баланс на кошельках
            //try
            //{
            //    program.PurseInfoFilterTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X11. Информация из аттестата
            //try
            //{
            //    program.PassportFinderTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X13. Возврат платежа с протекцией
            //try
            //{
            //    program.ProtectionRejectorTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X14. Бескомиссионный возврат средств
            //try
            //{
            //    program.TransferRejectorTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X15.1 Список моих доверенностей
            //// X15.2 Список доверяющих мне
            //// X15.3 Создание или изменение доверенности 
            //try
            //{
            //    program.TrustTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X16. Создание кошелька
            //try
            //{
            //    program.OriginalPurseTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X17.1 Создание контракта
            //// X17.2 Информация об акцептантах
            //try
            //{
            //    program.ContractTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            // X18.Детали операции через merchant
            try
            {
                program.MerchantOperationObtainerTest();
            }
            catch (WmException exception)
            {
                Console.WriteLine("ex");
                //Console.WriteLine(exception.TranslateExtendedErrorNumber(Language.Ru));
            }

            //// X19. Проверка данных WMID
            //try
            //{
            //    program.ClientInspectorTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X20.Оплата без покидания сайта
            //try
            //{
            //    program.ExpressPaymentTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //// X21. Выписка доверия через SMS.
            //try
            //{
            //    program.ExpressTrustTest();
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            return;

            //// X22.Получение ссылки на оплату через Merchant
            try
            {
                program.TestMerchantPayment();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //// X1. Выписка счета
        //// X4. История выписанных счетов (поиск по номеру счета)
        //// Отмена счета (недокументированный интерфейс)
        //public void OriginalInvoiceTest()
        //{
        //    var amount = (Amount)0.01;

        //    // X1. Выписка счета
        //    _orderId++;
        //    var originalInvoice = new OriginalInvoice(_orderId, _secondaryWmId, _primaryPurse, amount);
        //    originalInvoice.Description = (Description)("Проверка " + _orderId);
        //    originalInvoice.Address = (Description)"Адрес";
        //    originalInvoice.Expiration = 3;

        //    var recentInvoice = originalInvoice.Submit();

        //    if (!recentInvoice.Invoice.Description.Equals(originalInvoice.Description))
        //        Console.WriteLine("!recentInvoice.Description.Equals(originalInvoice.Description)");

        //    // X4. История выписанных счетов (поиск по номеру счета)
        //    var outgoingInvoiceFilter = new OutgoingInvoiceFilter(_primaryPurse, DateTime.Now.AddMinutes(-5), DateTime.Now);
        //    outgoingInvoiceFilter.InvoiceId = recentInvoice.Invoice.Id;
        //    var outgoingInvoiceRegister = outgoingInvoiceFilter.Submit();

        //    if (1 != outgoingInvoiceRegister.OutgoingInvoiceList.Count)
        //        Console.WriteLine("1 != outgoingInvoiceRegister.OutgoingInvoiceList.Count");

        //    var outgoingInvoice = outgoingInvoiceRegister.OutgoingInvoiceList[0];

        //    if (outgoingInvoice.Amount != recentInvoice.Invoice.Amount)
        //        Console.WriteLine("outgoingInvoice.Amount != recentInvoice.Amount");

        //    // Отменяем счет (недокументированный интерфейс)
        //    var invoiceRefusal = new InvoiceRefusal(outgoingInvoice.SourceWmId, recentInvoice.Invoice.Id);
        //    var invoiceReport = invoiceRefusal.Submit();

        //    if (InvoiceState.Refusal != invoiceReport.State)
        //        Console.WriteLine("InvoiceState.Refusal != invoiceReport1.State");
        //}

        //// X2. Перевод средств
        //// X3. История операций
        //public void OriginalTransferTest()
        //{
        //    var amount = (Amount) 0.01;

        //    // X2. Перевод средств ///////////////////////////////////////////////////////////////////////////////////////
        //    _tranId++;
        //    var originalTransfer1 = new OriginalTransfer(_tranId, _primaryPurse, _secondaryPurse, amount);
        //    originalTransfer1.Description = (Description)"Тестовая операция";
        //    originalTransfer1.AuthorizationRequired = true;

        //    var recentTransfer1 = originalTransfer1.Submit();

        //    // X2. Перевод средств по счету /////////////////////////////////////////////////////////////////////////////

        //    // X1. Выписка счета
        //    _orderId++;
        //    var originalInvoice = new OriginalInvoice(_orderId, _primaryWmId, _secondaryPurse, amount);
        //    originalInvoice.Description = (Description)("Проверка выписки счета" + _orderId);
        //    originalInvoice.Address = (Description)"Адрес";
        //    originalInvoice.Expiration = 3;

        //    var recentInvoice = originalInvoice.Submit();

        //    // X2.
        //    _tranId++;
        //    var originalTransfer2 = new OriginalTransfer(_tranId, _primaryPurse, _secondaryPurse, amount);
        //    originalTransfer2.Description = (Description)"Проверка оплаты счета";
        //    originalTransfer2.InvoiceId = recentInvoice.Invoice.Id;

        //    var recentTransfer2 = originalTransfer2.Submit();

        //    // X3. История операций

        //    var transferFilter = new TransferFilter(_primaryPurse, DateTime.Now.AddMinutes(-5), DateTime.Now);
        //    var transferRegister = transferFilter.Submit();

        //    bool exists1 = false;
        //    bool exists2 = false;

        //    foreach (var transfer in transferRegister.TransferList)
        //    {
        //        if (recentTransfer1.Transfer.Id == transfer.Id
        //            && recentTransfer1.Transfer.TargetPurse == transfer.TargetPurse
        //            && recentTransfer1.Transfer.Amount == transfer.Amount)
        //            exists1 = true;

        //        if (recentTransfer2.Transfer.Id == transfer.Id
        //            && recentTransfer2.Transfer.TargetPurse == transfer.TargetPurse
        //            && recentTransfer2.Transfer.Amount == transfer.Amount)
        //            exists2 = true;
        //    }

        //    if (!exists1)
        //        Console.WriteLine("!exists1");

        //    if (!exists2)
        //        Console.WriteLine("!exists2");
        //}

        //// X10. Список счетов на оплату
        //// Отменяем все счета (чистка)
        //public void InvoiceRefusalTest()
        //{
        //    // X10. Список счетов на оплату
        //    var incomingInvoiceFilter = new IncomingInvoiceFilter(_primaryWmId, DateTime.Now.AddDays(-1), DateTime.Now);
        //    var incomingInvoiceRegister = incomingInvoiceFilter.Submit();

        //    foreach (var incomingInvoice in incomingInvoiceRegister.IncomingInvoiceList)
        //    {
        //        if (InvoiceState.NotPaid == incomingInvoice.InvoiceState)
        //        {
        //            // Отменяем счет
        //            var invoiceRefusal = new InvoiceRefusal(incomingInvoice.TargetWmId, incomingInvoice.Id);
        //            var invoiceReport = invoiceRefusal.Submit();

        //            if (InvoiceState.Refusal != invoiceReport.State)
        //                Console.WriteLine("InvoiceState.Refusal != invoiceReport1.State");
        //        }
        //    }
        //}

        //// X5. Завершение операции с протекцией
        //public void ProtectionFinisherTest()
        //{
        //    var amount = (Amount) 0.01;

        //    var code = (Description)"код протекции";

        //    _tranId++;
        //    var originalTransfer = new OriginalTransfer(_tranId, _primaryPurse, _secondaryPurse, amount);
        //    originalTransfer.Description = (Description)"Проверка завершения кода протекции";
        //    originalTransfer.Code = code;
        //    originalTransfer.Period = 1;

        //    var recentTransfer = originalTransfer.Submit();

        //    if (TransferType.Protection != recentTransfer.Transfer.TransferType)
        //        Console.WriteLine("TransferType.Protection != protectionReport.TransferType");

        //    var protectionFinisher = new ProtectionFinisher(recentTransfer.Transfer.Id, code);
        //    var protectionReport = protectionFinisher.Submit();

        //    if (TransferType.Normal != protectionReport.TransferType)
        //        Console.WriteLine("TransferType.Normal != protectionReport.TransferType");
        //}

        //// X6. Отправка сообщения
        //public void MessageTest()
        //{
        //    var subject = (Description)"Проверка";
        //    var content = (Message)"Текст сообщения (не более 1024 символов).";

        //    var originalMessage = new OriginalMessage(_secondaryWmId, subject, content);
        //    var recentMessage = originalMessage.Submit();

        //    if (!recentMessage.Subject.Equals(subject))
        //        Console.WriteLine("!recentMessage.Subject.Equals(subject)");

        //    if (!recentMessage.Content.Equals(content))
        //        Console.WriteLine("!recentMessage.Subject.Equals(subject)");
        //}

        //// X7. Проверка подписи
        //public void SignatureInspectorTest()
        //{
        //    var signer = new Signer();
        //    signer.Initialize(
        //        "<RSAKeyValue><Modulus>ccqACxc2qzE3t+zJntCBGrw/qZTmrmJEPhmkVGUwhc+ZhDiIr4qwAvhnNwIme9r5YjWEdi7wm/XDnsp096aNjlkI</Modulus><D>FW1RaQMqZ6zHl6+wx+16uDB4zAmRr9sbBP7xVxzIg4vuIG4/w5b5XzFNv2sfSsmu+I3roO7E7qYmoeIn8otCeHgD</D></RSAKeyValue>");

        //    string message1 = "Сообщение1";
        //    string message2 = "Сообщение2";
        //    string signature = signer.Sign(message1);

        //    var signatureInspector1 = new SignatureInspector(WmId.Parse("712300479010"), (Message)message1,
        //                                                     (Description)signature);
        //    var signatureEvidence1 = signatureInspector1.Submit();

        //    var signatureInspector2 = new SignatureInspector(WmId.Parse("712300479010"), (Message)message2,
        //                                                     (Description)signature);
        //    var signatureEvidence2 = signatureInspector2.Submit();

        //    if (!signatureEvidence1.VerificationResult)
        //        Console.WriteLine("!signatureEvidence1.VerificationResult");

        //    if (signatureEvidence2.VerificationResult)
        //        Console.WriteLine("signatureEvidence2.VerificationResult");
        //}

        //// X8. Поиск по идентификатору или кошельку
        //public void WmIdFinderTest()
        //{
        //    var wmIdFinder1 = new WmIdFinder(WmId.Parse("712300479010"));
        //    var wmIdReport1 = wmIdFinder1.Submit();

        //    var wmIdFinder2 = new WmIdFinder(WmId.Parse("700300499010"));
        //    var wmIdReport2 = wmIdFinder2.Submit();

        //    var wmIdFinder3 = new WmIdFinder(Purse.Parse("R785098712289"));
        //    var wmIdReport3 = wmIdFinder3.Submit();

        //    var wmIdFinder4 = new WmIdFinder(Purse.Parse("R700300499010"));
        //    var wmIdReport4 = wmIdFinder4.Submit();
        //}

        //// X9. Баланс на кошельках
        //public void PurseInfoFilterTest()
        //{
        //    var purseInfoFilter = new PurseInfoFilter(_primaryWmId);
        //    var purseInfoRegister = purseInfoFilter.Submit();
        //}

        //// X11. Информация из аттестата
        //public void PassportFinderTest()
        //{
        //    var passportFinder = new PassportFinder(_secondaryWmId);
        //    var passport = passportFinder.Submit();
        //}

        //// X13. Возврат платежа с протекцией
        //public void ProtectionRejectorTest()
        //{
        //    var amount =(Amount)0.01;

        //    var code = (Description)"код протекции";

        //    _tranId++;
        //    var originalTransfer = new OriginalTransfer(_tranId, _primaryPurse, _secondaryPurse, amount);
        //    originalTransfer.Description = (Description)"Проверка завершения кода протекции";
        //    originalTransfer.Code = code;
        //    originalTransfer.Period = 1;

        //    var recentTransfer = originalTransfer.Submit();

        //    if (TransferType.Protection != recentTransfer.Transfer.TransferType)
        //        Console.WriteLine("TransferType.Protection != protectionReport.TransferType");

        //    // X12. Возврат платежа с протекцией
        //    var protectionRejector = new ProtectionRejector(recentTransfer.Transfer.Id);
        //    var protectionReport = protectionRejector.Submit();


        //    if (TransferType.ProtectionCancel != protectionReport.TransferType)
        //        Console.WriteLine("TransferType.ProtectionCancel != protectionReport.TransferType");
        //}

        //// X14. Бескомиссионный возврат средств
        //public void TransferRejectorTest()
        //{
        //    var amount =  (Amount)0.01;

        //    var code = (Description)"код протекции";

        //    _tranId++;
        //    var originalTransfer = new OriginalTransfer(_tranId, _primaryPurse, _secondaryPurse, amount);
        //    originalTransfer.Description = (Description)"Бескомиссионный возврат средств -- проверка";

        //    var recentTransfer = originalTransfer.Submit();

        //    if (TransferType.Normal != recentTransfer.Transfer.TransferType)
        //        Console.WriteLine("TransferType.Protection != protectionReport.TransferType");

        //    var transferRejector = new TransferRejector(recentTransfer.Transfer.Id, amount);
        //    var moneybackReport = transferRejector.Submit();

        //    if (!moneybackReport.Description.ToString().StartsWith("Moneyback transaction WMTranId"))
        //        Console.WriteLine(moneybackReport.Description);
        //}

        //// X15.1 Список моих доверенностей
        //// X15.2 Список доверяющих мне
        //// X15.3 Создание или изменение доверенности 
        //public void TrustTest()
        //{
        //    var outgoingTrustFilter = new OutgoingTrustFilter(_primaryWmId);
        //    var trustRegister1 = outgoingTrustFilter.Submit();

        //    var incomingTrustFilter = new IncomingTrustFilter(_primaryWmId);
        //    var trustRegister2 = incomingTrustFilter.Submit();

        //    var originalTrust = new OriginalTrust(_secondaryWmId, _primaryPurse);
        //    originalTrust.InvoiceAllowed = true;
        //    var trustRegister3 = originalTrust.Submit();
        //}

        //// X16. Создание кошелька
        //public void OriginalPurseTest()
        //{
        //    var originalPurse = new OriginalPurse(_primaryWmId, WmCurrency.Y, (Description)"Temp");
        //    var recentPurse = originalPurse.Submit();
        //}

        //// X17.1 Создание контракта
        //// X17.2 Информация об акцептантах
        //public void ContractTest()
        //{
        //    var originalContract = new OriginalContract((Description)"Тестовый контракт", ContractType.Private,
        //                                                "Контракт создан в целях тестирования библиотеки WM-API.");

        //    originalContract.AcceptorList = new List<WmId>();
        //    originalContract.AcceptorList.Add(_primaryWmId);
        //    originalContract.AcceptorList.Add(_secondaryWmId);

        //    var recentContract = originalContract.Submit();

        //    var acceptorFilter = new AcceptorFilter(recentContract.ContractId);
        //    var acceptorRegister = acceptorFilter.Submit();

        //    //var acceptorFilter = new AcceptorFilter(1161942);
        //    //var acceptorRegister = acceptorFilter.Submit();
        //}

        // X18. Детали операции через merchant
        public void MerchantOperationObtainerTest()
        {
            //var transferFilter = new TransferFilter(_primaryPurse, DateTime.Now.AddMonths(-17), DateTime.Now.AddMonths(-16));
            //var transferRegister = transferFilter.Submit();
            
            var merchantOperationFilter = new MerchantOperationObtainer(_storePurse, 17, 1);

            try
            {
                var merchantOperation = merchantOperationFilter.Submit();
                Console.WriteLine(merchantOperation.Amount);
            }
            catch (MerchantOperationObtainerException e)
            {
                Console.WriteLine(e.TranslateExtendedErrorNumber(Language.Ru));
            }
        }
        
        //// X19. Проверка данных WMID
        //public void ClientInspectorTest()
        //{
        //    var amount = (Amount)0.01;

        //    ClientInspector clientInspector;
        //    ClientEvidence clientEvidence;

        //    // Ввод/вывод WM наличными в одном из обменных пунктов
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, (Description) "AA000001",
        //        _clientSecondName, _clientFirstName);
        //    clientInspector.Output = false;
        //    clientEvidence = clientInspector.Submit();

        //    // Ввод/вывод WM наличными через системы денежных переводов
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, _clientSecondName, _clientFirstName);
        //    clientInspector.Output = false;
        //    clientEvidence = clientInspector.Submit();

        //    // Ввод/вывод WM на банковский счет
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, _clientSecondName, _clientFirstName, (Description)"ПриватБанк", BankAccount.Parse("111222"));
        //    clientInspector.Output = false;
        //    clientEvidence = clientInspector.Submit();

        //    // Ввод/вывод WM на банковскую карту
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, _clientSecondName, _clientFirstName, (Description)"ПриватБанк", _clientBankCard);
        //    clientInspector.Output = false;
        //    clientEvidence = clientInspector.Submit();

        //    // Обмен WM на электронную валюту других систем
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, PaymentSystem.YandexMoney, (Description)"41001203776406");
        //    clientInspector.Output = false;
        //    string s = clientInspector.Compile();

        //    // Ввод WM за SMS (только operation/direction=2)
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, "+380440000001");
        //    clientInspector.Output = false;
        //    clientEvidence = clientInspector.Submit();

        //    // Пополнение телефона
        //    clientInspector = new ClientInspector(WmCurrency.U, amount, _secondaryWmId, "+380440000001", true);
        //    clientInspector.Output = true;
        //    clientEvidence = clientInspector.Submit();
        //}

        //// X20. Оплата без покидания сайта
        //public void ExpressPaymentTest()
        //{
        //    var expressPaymentRequest = new ExpressPaymentRequest(
        //        _storePurse,
        //        0,
        //        (Amount)2.01M,
        //        (Description)"Проверка ExpressPayment",
        //        _clientPhone,
        //        ConfirmationType.SMS,
        //        CultureInfo.GetCultureInfo("ru-RU"));

        //    var expressPaymentResponse = expressPaymentRequest.Submit();

        //    Console.WriteLine(expressPaymentResponse.Info);
        //    Console.WriteLine(expressPaymentResponse.InvoiceId);

        //    string confirmationCode = null;

        //    if (ConfirmationType.SMS == expressPaymentResponse.ConfirmationType)
        //        confirmationCode = Console.ReadLine();

        //    var expressPaymentConfirmation = new ExpressPaymentConfirmation(
        //        _storePurse,
        //        confirmationCode,
        //        expressPaymentResponse.InvoiceId,
        //        CultureInfo.CreateSpecificCulture("ru-RU"));

        //    string c1 = expressPaymentConfirmation.Compile();
        //    Console.WriteLine(c1);

        //    var expressPaymentReport = expressPaymentConfirmation.Submit();

        //    Console.WriteLine(expressPaymentReport.SmsState);
        //}

        //// X21. Выписка доверия через SMS.
        //public void ExpressTrustTest()
        //{
        //    var expressTrustRequest = new ExpressTrustRequest(
        //        _storePurse,
        //        (Amount)0.01,
        //        (Amount)0.0,
        //        (Amount)0.0,
        //        _clientPhone,
        //        ConfirmationType.SMS,
        //        CultureInfo.CreateSpecificCulture("ru-RU"));

        //    var expressTrustResponse = expressTrustRequest.Submit();

        //    Console.WriteLine(expressTrustResponse.Info);

        //    string confirmationCode = null;

        //    if (ConfirmationType.SMS == expressTrustResponse.ConfirmationType)
        //        confirmationCode = Console.ReadLine();

        //    var expressTrustConfirmation = new ExpressTrustConfirmation(
        //        expressTrustResponse.Reference, confirmationCode, CultureInfo.CreateSpecificCulture("ru-RU"));

        //    var expressTrustReport = expressTrustConfirmation.Submit();

        //    Console.WriteLine(expressTrustReport.TrustId);
        //}

        // X22. Получение ссылки на оплату через Merchant
        public void TestMerchantPayment()
        {
            var originalMerchantPayment = new OriginalMerchantPayment(
                107, _storePurse, (Amount)10.0, (Description)"Тестовый платеж (проверка API)", 1);

            var merchantPaymentToken = originalMerchantPayment.Submit();
            Console.WriteLine(merchantPaymentToken.Token);
        }
    }
}

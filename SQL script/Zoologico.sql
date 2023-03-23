CREATE DATABASE  IF NOT EXISTS `zoologico` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `zoologico`;
-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: localhost    Database: zoologico
-- ------------------------------------------------------
-- Server version	8.0.29

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `animal`
--

DROP TABLE IF EXISTS `animal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `animal` (
  `Id` int NOT NULL AUTO_INCREMENT COMMENT 'Es el campo Id, es auto incrementable, por lo que se genera de manera automatica.',
  `Nombre` varchar(40) NOT NULL COMMENT 'Es el campo que guarda el nombre del animal que se quiere almacenar.',
  `IdHabitat` int NOT NULL COMMENT 'Este es un campo foraneo que se vincula al Id de la tabla Habitat para ligar éste animal a cierto hábitat.',
  `Peso` double DEFAULT NULL COMMENT 'Este campo muestra la cantidad de kilogramos que pesa el animal, puede contener numero de punto flotante.',
  `TipoAlimentacion` varchar(40) DEFAULT NULL COMMENT 'Este campo describe el tipo de alimentcaión del animal almacenado.',
  `NivelPeligroDeExtincion` varchar(40) DEFAULT NULL COMMENT 'Este campo muestra de manera textual, el peligro que corre el animal para extinguirse.',
  PRIMARY KEY (`Id`),
  KEY `fkHabitatAnimal` (`IdHabitat`),
  CONSTRAINT `fkHabitatAnimal` FOREIGN KEY (`IdHabitat`) REFERENCES `habitat` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `animal`
--

LOCK TABLES `animal` WRITE;
/*!40000 ALTER TABLE `animal` DISABLE KEYS */;
INSERT INTO `animal` VALUES (5,'León',4,50,'Carnivoro','Peligro crítico'),(12,'Gacela',4,100,'Carnivoro','Bajo'),(13,'Zorro',3,10,'Carnivoro','Bajo'),(17,'Oso marino',8,10,'Carnivoro','Bajo'),(19,'Animal',8,10,'Carnivoro','Bajo'),(22,'Otro oso',3,10,'Carnivoro','Bajo'),(23,'Águila arpía',6,15,'Carroñero','Bajo'),(24,'Armadillo',6,9,'Hervivoro','Bajo'),(25,'Chimpancé',6,47,'Omnivoro','Bajo'),(26,'Gorila',6,99,'Omnivoro','Bajo'),(27,'Osos Hormigueros',7,40,'Insectívoros','Alto'),(28,'Coyotes',7,50,'Omnivoro','Bajo'),(29,'Estrella de mar',11,2,'Omnivoro','Alto'),(30,'Tiburón blanco',11,220,'Carnivoro','Alto'),(31,'Pulpo ',8,7,'Omnivoro','Medio'),(32,'Medusa ',8,2,'Omnivoro','Medio'),(33,'Tortuga caguama',8,12,'Omnivoro','Medio'),(34,'Pez erizo',8,2,'Omnivoro','Bajo'),(35,'Osos grizzly',13,110,'Carnivoro','Alto'),(36,'Alces',13,200,'Hervivoro','Bajo'),(37,'Ciervo de cola blanca',13,80,'Hervivoro','Bajo'),(38,'Pingüino',15,40,'Omnivoro','Bajo');
/*!40000 ALTER TABLE `animal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `habitat`
--

DROP TABLE IF EXISTS `habitat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habitat` (
  `Id` int NOT NULL AUTO_INCREMENT COMMENT 'Es el campo Id, es auto incrementable, por lo que se genera de manera automatica.',
  `Nombre` varchar(40) NOT NULL COMMENT 'Este campo muestra el nombre del hábitat que se almacena en la base de datos.',
  `TipoHabitat` text COMMENT 'Este campo describe de manera breve el tipo de habitat que es (clima, relieve, etc.).',
  `Capacidad` int NOT NULL COMMENT 'Este campo muestra la cantidad de animales que pueden estar ligados a un hábitat, éste se controla con procedimientos almacenados al agregar y eliminar.',
  `Vegetacion` text COMMENT 'Este campo describe de manera breve la vegetación con la que cuenta el hábitat.',
  `Tamano` double DEFAULT NULL COMMENT 'Este campo es el tamaño en metros cuadrados (m2) la amplitud del hábitat almacenado.',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habitat`
--

LOCK TABLES `habitat` WRITE;
/*!40000 ALTER TABLE `habitat` DISABLE KEYS */;
INSERT INTO `habitat` VALUES (3,'Artico','Frio',0,'Sin vegetación',30),(4,'Sabana','Estepas',2,'Sabana, Parque y Estepa Arbustiva',40),(6,'Selva','Tropical',4,'Lianas',50),(7,'Desierto','Área de tierra extremadamente seca y con escasas precipitaciones',3,'Plantas espinosas, como los cactus y matorrales',40),(8,'Acuario','Hábitat acuático artificial	Musgos, helechos, tapizantes',0,'Algas',30),(9,'Bosque templado','Zona con clima templado, enre clima cálido y frío',3,'Predominan en las zonas altas de este ecosistema los pinos comunes: ocotes blanco, chino y pardo, cedrón, acahuite.',50),(10,'Pantano','No es un hábitat entramente acuático ni completamente terrestre. Algunos pantanos están siempre cubiertos por agua, pero otros pueden tener el agua estancada durante ciertas épocas del año y durante otros, es el suelo el que se encuentra saturado con el líquido o ligeramente encharcado.',3,'Cipreses, robles, lentejas de agua, musgos y otras especies vegetales.',60),(11,'Oceano','Hábitat acondicionado para animales del oceano',3,'Algas verdes o clorofitas, el fitoplancton, el árbol solitario, el canelo de Magallanes y el castaño del Pacífico.',70),(12,'Tundra','El clima es frío y ventoso, y las precipitaciones son escasas.',3,'Sin vegetación',80),(13,'Montañas','Por su altura se clasifican en colinas, montañas medias, y montañas altas. Por su forma de agrupación tenemos las cordilleras',3,'Suele presentarse de manera escalonada, por peldaños, a medida que se asciende por la ladera',60),(15,'Habitat de pingüinos','Frio',3,'Sin vegetación',60);
/*!40000 ALTER TABLE `habitat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'zoologico'
--

--
-- Dumping routines for database 'zoologico'
--
/*!50003 DROP PROCEDURE IF EXISTS `spAgregarAnimal` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spAgregarAnimal`(paIdHabitat int,paNombre varchar(40),paPeso float, paTipoAlimentacion varchar(40), paNivelPeligroExtincion varchar(40))
BEGIN
select Capacidad into @Cap
from Habitat
where Id=paIdHabitat;

if (@Cap!=0) then
begin

insert into Animal (Nombre, IdHabitat, Peso, TipoAlimentacion, NivelPeligroDeExtincion) 
values (paNombre, paIdHabitat, paPeso, paTipoAlimentacion, paNivelPeligroExtincion);

update Habitat 
set Capacidad = Capacidad - 1
where Id = paIdHabitat;


end;

else
begin


end;

end if;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spEliminarAnimal` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spEliminarAnimal`(paId int)
BEGIN
declare paIdHabitat int;
select IdHabitat into paIdHabitat from Animal where Id=paId;

update Habitat
set Capacidad = Capacidad + 1
where Id = paIdHabitat;

delete from Animal
where Id=paId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-22 22:35:56

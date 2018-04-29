--==============================================================
--uncomment section below if you need to create NEW database....
--this will create new database called "Brewerys"
--==============================================================

--USE [master]
--GO
--CREATE DATABASE [Brewery]
--GO
--USE [Brewery]
--GO

----========================================
----==== END OF CREATE DATABASE SECTION ====
----========================================




----=====================================================================================
----uncomment section below if database exists and your running new table creation....
----this will remove old tables/data and create new Tables in "Brewery" with initial data
----=====================================================================================

--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE reviews
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;
--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE beerPhotos
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;
--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE breweryPhotos
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;
--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE beers
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;
--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE users
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;
--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE operation
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;
--BEGIN TRY BEGIN TRANSACTION;
--DROP TABLE breweries
--COMMIT;END TRY BEGIN CATCH ROLLBACK;END CATCH;

----========================================
----==== END OF DELETE EXISITING TABLES ====
----========================================





----========================================
----====  TABLE AND DATA SCRIPT BELOW  =====
----========================================

BEGIN TRY
    BEGIN TRANSACTION;
CREATE TABLE breweries
(
  id            INT IDENTITY( 1, 1 ),
  name          VARCHAR(100) NOT NULL,
  history       VARCHAR(MAX) NULL,
  imagery       VARCHAR(MAX) NULL,
  address       VARCHAR(300) NULL,
  contact_name  VARCHAR(100) NULL,
  contact_email VARCHAR(100) NULL,
  contact_phone VARCHAR(30) NULL,
  CONSTRAINT pk_breweries PRIMARY KEY( id ), );

CREATE TABLE operation
(
  brewery_id INT NOT NULL,
  DAY        VARCHAR(12) NULL,
  opens      VARCHAR(50) NULL,
  closes     VARCHAR(50) NULL,
  CONSTRAINT fk_operation_breweries FOREIGN KEY( brewery_id ) REFERENCES breweries( id ));

CREATE TABLE users
(
  id         INT IDENTITY( 1, 1 ),
  email      VARCHAR(200)
  UNIQUE NOT NULL,
  username   VARCHAR(100) NOT NULL,
  password   VARCHAR(200) NOT NULL,
  is_brewer  BIT NOT NULL
                 DEFAULT 0,
  brewery_id INT NULL,
  is_admin   BIT NOT NULL
                 DEFAULT 0,
  CONSTRAINT pk_users PRIMARY KEY( id ),
  CONSTRAINT fk_users_breweries FOREIGN KEY( brewery_id ) REFERENCES breweries( id ));

CREATE TABLE beers
(
  id          INT IDENTITY( 1, 1 ),
  name        VARCHAR(MAX) NOT NULL,
  description VARCHAR(MAX) NOT NULL,
  IMAGE       VARCHAR(100) NULL,
  abv         VARCHAR(20) NULL,
  --abv         REAL NULL,
  beer_type   VARCHAR(100) NOT NULL,
  brewery_id  INT NOT NULL,
  show_hide   BIT NOT NULL
                  DEFAULT 1,
  CONSTRAINT pk_beers PRIMARY KEY( id ),
  CONSTRAINT fk_beers_brewery_info FOREIGN KEY( brewery_id ) REFERENCES breweries( id ));

CREATE TABLE breweryPhotos
(
  FILE_NAME   VARCHAR(100) NULL,
  brewery_id  INT NULL,
  profile_pic BIT NULL
                  DEFAULT 0,
  CONSTRAINT fk_breweryPhotos_breweries FOREIGN KEY( brewery_id ) REFERENCES breweries( id ), );

CREATE TABLE beerPhotos
(
  FILE_NAME VARCHAR(100) NULL,
  beer_id   INT NULL,
  CONSTRAINT fk_beerPhotos_breweries FOREIGN KEY( beer_id ) REFERENCES beers( id ), );

CREATE TABLE reviews
(
  id      INT IDENTITY( 1, 1 ),
  USER_ID INT NOT NULL,
  beer_id INT NOT NULL,
  rating  INT NOT NULL,
  review  VARCHAR(MAX) NOT NULL,
  CONSTRAINT pk_reviews PRIMARY KEY( id ),
  CONSTRAINT fk_reviews_user_info FOREIGN KEY( USER_ID ) REFERENCES users( id ),
  CONSTRAINT fk_reviews_beers FOREIGN KEY( beer_id ) REFERENCES beers( id ), );

      COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
END CATCH;



-----------------------------ADD DATA TO TABLES BELOW----------------------------------------
----  | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | |  --
----  V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V V  --


--------------------------------BREWERIES
BEGIN TRY
    BEGIN TRANSACTION;
INSERT INTO breweries
(name,
 history,
 address,
 contact_name,
 contact_email,
 contact_phone)
VALUES
(
  'MadTree Brewing', 'Taproom, carryout, café, family hangout, meeting place… the MadTree taproom is a little bit of everything. Whether you want to fill up your growler or meet up with some of your friends for dinner and a beer, we are happy to have you drop in. ', '3301 Madison Rd, Cincinnati, OH 45209', 'Mataan', 'MadTreeBrew@OTR.com', '(513) 836-8733'),
(
  'The Woodburn Brewery', 'Legend tells us the phoenix was a creature of unmatched beauty, living for a thousand years before going out in a blaze of glory. And just when you thought that bird was dead, it would rise from its own ashes, full of new life. That’s why we chose the phoenix as our brand mark. We believe in the rebirth of Cincinnati’s proud brewing legacy. The best is yet to come. And it’ll be delicious.', '2800 Woodburn Ave, Cincinnati, OH 45206', 'Robert', 'WoodburnBrewery@OTR.com', '(513) 221-2337'),
(
  'Nine Giant', 'Nine Giant started with a simple question:  “What would make the ultimate taproom experience?”  For us, the answers were pretty easy.  Constant evolution of hand-crafted beers.  Scratch-made food, sourced from the highest quality ingredients.  A smaller, more intimate environment.  And a glistening wall of blue subway tile.', '6095 Montgomery Rd, Cincinnati, OH 45213', 'Adam', '9Giant@nine.com', '(513) 366-4550'),
(
  'Rhinegeist Brewery', 'Our name, Rhinegeist, translates to "Ghost of the Rhine" and refers to our place in the historic Over-the-Rhine Brewery District in Cincinnati. Built within the skeleton of the old Moerlein bottling plant (1895), we brew batches of beer that sing with flavor.', '1910 Elm St, Cincinnati, OH 45202', 'Mary Rachel', 'MadTreeBrew@OTR.com', '(513) 381-1367');

        COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
END CATCH;


--------------------------------BEERS
BEGIN TRY
    BEGIN TRANSACTION;
INSERT INTO beers
(name,
 description,
 abv,
 beer_type,
 brewery_id)
VALUES
(
  'PSYCHOPATHY', 'Explore the blend of citrus, bright, and floral aromas. Additional citrus flavors follow alongside a smooth bitterness. It finishes with a medium body and a slightly sweet, malty backbone. Trust your senses.', '6.9%', 'IPA', 1),
(
  'LIFT', 'Whether taking a break from a hard day of work or just a huge day of hops, this crisp refreshing beer with a hint of orange will lift your spirits without lowering your expectations for what a craft beer should be.', '4.7%', 'KÖLSCH', 1),
(
  'PSA', 'PSA was designed to be just that, a Public Service Announcement that craft beer can be both thoughtful and approachable. ', '4.5%', 'PALE ALE', 1),
(
  'HAPPY AMBER', 'This dry-hopped ale combines carmel and biscuit malt flavors in happy equilibrium with late addition American hops. The approachable balance is intentional although the name is "hoppy accident."', '6%', 'AMBER', 1);

INSERT INTO beers
(name,
 description,
 abv,
 beer_type,
 brewery_id)
VALUES
(
  'BERLINER WEISSE', 'Tart, bubbly, and refreshing Berliner.', '4.0%', 'WHEAT', 2),
(
  'BREAKFAST PORTER', 'Notes of chocolate, honey, and graham cracker…said to taste like a Teddy Graham.', '6.3%', 'PORTER', 2),
(
  'CEDAR IPA', 'Earthy and life-changing.', '6.8%', 'IPA', 2),
(
  'HAMMER SESSION IPA', 'Juicy, floral, citrus.', '5.3%', 'IPA', 2);

INSERT INTO beers
(name,
 description,
 abv,
 beer_type,
 brewery_id)
VALUES
(
  'CREAM', 'Light and tasty. Nice way to start the night.', '5.2%', 'Cream Ale', 3),
(
  'Electrify', 'A beautifully hazy pale ale featuring Wakatu, Motueka and Idaho 7 hops for aromas of fresh crushed citrus and orange blossom.', '5%', 'PALE ALE', 3),
(
  'Infinite Spark', 'Our latest one-off IPA series featuring Galaxy and Columbus hops, ripe with aromas of tangerine peel and juicy, dank herbaciousness.', '6.6%', 'IPA ', 3),
(
  'Codex', 'A crisp, dry saison with aromas of fresh tangerine, honeysuckle and lilac', '5.5%', 'FARMHOUSE ALE', 3);

INSERT INTO beers
(name,
 description,
 abv,
 beer_type,
 brewery_id)
VALUES
(
  'TRUTH', 'Brewed with a nod to the Pacific, hops sizzle with tropical fruit aroma, grapefruit and mango notes and a dry finish.', '7.2%', 'IPA', 4),
(
  'COUGAR', 'Hopped with arguable restraint with Crystal and Bravo hops, Cougar stays fierce enough to raise eyebrows yet is tame enough to be called sessionable.', '4.8%', 'BLONDE ALE', 4),
(
  'BUBBLES', 'A Rosé Ale made with apple, peach and cranberry, for additional pink hue, tartness and juicy fruitiness.', '6.2%', 'ROSÉ ALE', 4),
(
  'HUSTLE', 'This India Pale Ale takes a hard, hoppy cut at a soft, fluffy hanging curve, driving notes of peach, tangerine and citrus back, back, back...gone.', '6.0%', 'IPA', 4);

        COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
END CATCH;

--------------------------------USERS
BEGIN TRY
    BEGIN TRANSACTION;
INSERT INTO users
(email,
 username,
 password,
 is_brewer,
 brewery_id,
 is_admin)
VALUES
(
  'admin@admin.com', 'Administrator', 'password', 1, NULL, 1),
(
  'beerlover@user.com', 'Beer Lover #1', 'password', 0, NULL, 0);

        COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
END CATCH;



select * from breweries
select * from users
select * from beers






/*
 Navicat Premium Data Transfer

 Source Server         : Casa
 Source Server Type    : MySQL
 Source Server Version : 100411
 Source Host           : localhost:3306
 Source Schema         : vorp

 Target Server Type    : MySQL
 Target Server Version : 100411
 File Encoding         : 65001

 Date: 27/06/2020 19:08:15
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for banneds
-- ----------------------------
DROP TABLE IF EXISTS `banneds`;
CREATE TABLE `banneds`  (
  `b_id` int(11) NOT NULL AUTO_INCREMENT,
  `b_steam` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `b_license` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `b_discord` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `b_banned` datetime(0) NOT NULL,
  `b_unban` datetime(0) NOT NULL,
  `b_permanent` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`b_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;

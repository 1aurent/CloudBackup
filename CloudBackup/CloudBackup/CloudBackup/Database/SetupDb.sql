CREATE TABLE IF NOT EXISTS Version(
	id integer not null primary key,
	value text not null
);

CREATE TABLE IF NOT EXISTS Settings(
	id    text not null primary key,
	value text not null
);

INSERT OR IGNORE INTO Version Values ('SshHost','changeme');
INSERT OR IGNORE INTO Version Values ('SshUser','changeme');
INSERT OR IGNORE INTO Version Values ('SshPwd', 'changeme');
INSERT OR IGNORE INTO Version Values ('SshPath','/home/changeme/');
INSERT OR IGNORE INTO Version Values ('IsGlacier', 'False');
INSERT OR IGNORE INTO Version Values ('ZipPwd', 'changeme');

INSERT OR IGNORE INTO Version Values (1, '1.0.0.0'); -- Database Version
INSERT OR IGNORE INTO Version Values (2, '1.0.0.0'); -- Database Compatibility Version
INSERT OR IGNORE INTO Version Values (3, '1.0.0.0'); -- Server Version

CREATE TABLE IF NOT EXISTS Schedule(
	id integer not null primary key,
	name text not null,
	schedule text not null,
	rootPath text not null,
	active integer not null
);
CREATE UNIQUE INDEX IF NOT EXISTS ScheduleNames ON Schedule(name);

CREATE TABLE IF NOT EXISTS SnapshotFile(
	id integer not null primary key,
	sourceSchedule  integer not null,
	name text not null,
	created integer not null
);
CREATE UNIQUE INDEX IF NOT EXISTS SnapshotFileIds ON SnapshotFile(sourceSchedule,name);

CREATE TABLE IF NOT EXISTS ArchiveFiles(
	sourcePath text not null,
	sourceSchedule  integer not null,
	fileSize integer not null,
	created integer not null,
	modified integer not null,
	notedHash integer not null,
	lastSnapshot integer not null,
	seen bit not null
);	
CREATE UNIQUE INDEX IF NOT EXISTS ArchiveFileIds ON ArchiveFiles(sourceSchedule,sourcePath);

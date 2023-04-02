# EmpyrionPrime.ModFramework Changelog
All notable changes to this project will be documented in this file.

The format is loosely based on [Keep a Changelog](http://keepachangelog.com/en/1.1.0/).

## Unreleased
### Removed
 - EleonExtensions, these can now be found in EmpyrionPrime.Plugin

## 1.0.1 (2023-04-01)
### Fixed
 - RequestBroker not handling some game requests that have no return value.
   This only affects Request_InGameMessage_[Target] requests.


## 1.0.0 (2023-03-31)
### Added
 - Add-on Empyrion API interfaces for ModInterface (legacy API)
   - IRequestBroker for simplified game requests
   - IApiRequests for async typed game requests
   - ApiEvents for typed game events
 - Empyrion server information (IEmpyrionEnvironment)
 - Plugin options system (IPluginOptionsFactory)
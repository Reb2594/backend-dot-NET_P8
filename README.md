# TourGuide

![CI Status](https://github.com/Reb2594/backend-dot-NET_P8/workflows/TourGuide%20CI/badge.svg)
![.NET](https://img.shields.io/badge/.NET-7.0-blue)

## Description

Ce projet s'inscrit dans le cadre de la formation OpenClassrooms. Il s'agit d'un projet d'amélioration de l'application **ASP.NET Core Web API TourGuide** face à une croissance du nombre d’utilisateurs.

**Tour Guide** est une application qui permet à ses utilisateurs de voir quelles sont les attractions touristiques à proximité et d’obtenir des réductions sur les séjours à l’hôtel ainsi que sur les billets de différents spectacles.

### Améliorations apportées

* **Correction des bugs des tests unitaires**
* **Correction du bug des destinations recommandées**
* **Optimisation des performances globales**
* **Mise à jour des tests suite aux optimisations**
* **Mise en place d'une pipeline d'intégration continue avec GitHub Actions**

## Technologies utilisées

* **.NET 7**
* **ASP.NET Core**
* **xUnit**  - Framework de tests
* **Coverlet**  - Analyse de couverture de code
* **GitHub Actions** - CI/CD

## Installation et exécution

### Prérequis

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- Git

### Étapes

1. **Cloner le dépôt**

```bash
git clone https://github.com/Reb2594/backend-dot-NET_P8.git
cd backend-dot-NET_P8
```

2. **Restaurer les dépendances**

```bash
dotnet restore TourGuide.sln
```
3. **Compiler l'application**

```bash
dotnet build
```

4. **Lancer l’application**

```bash
dotnet run --project TourGuide
```

5. **Exécuter les tests**

Le projet inclut des tests de performance pour valider les optimisations :

```csharp
[Fact]
public async Task HighVolumeTrackLocation()

[Fact]
public async Task HighVolumeGetRewards()
```

Note : Vous pouvez ajuster le nombre d'utilisateurs directement dans les méthodes cf. commentaire dans le code.

**Tests simples**

```bash
dotnet test
```

**Tests avec coverage**

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

**Tests avec rapport HTML**

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html
```

**Ouvrir coveragereport/index.html dans votre navigateur**

6. **Générer les fichiers de publication**

```bash
dotnet publish TourGuide.sln --configuration Release --output publish
```
Les fichiers seront disponibles dans le dossier publish/.

## Intégration Continue (GitHub Actions)

Une pipeline GitHub Actions est configurée pour s'exécuter automatiquement sur chaque **push** ou **pull request** vers les branches **master** ou **dev**.

**Fonctionnalités de la pipeline**
* Restauration des dépendances
* Compilation de la solution
* Exécution des tests avec couverture de code
* Indique le seuil de couverture ( <60% : insuffisant (mais pas bloquant) / 60%-80% : acceptable / >80% : bon )
* Publication et création d'un artifact TourGuide.zip
* Génération de rapports de tests et de couverture

**Consulter les résultats :**
* Allez dans l'onglet Actions du repository
* Sélectionnez le workflow souhaité
* Téléchargez les artifacts (rapports de couverture, application publiée)

## Structure du projet

```
/TourGuide                            => Projet principal
/TourGuideTest                        => Projet de tests unitaires
/GpsUtil, /RewardCentral, /TripPricer => Projets externes liés
/.github/workflows/ci.yml             => Fichier de configuration GitHub Actions
``` 

## Documentation

- TourGuide - Documentation fonctionnelle et technique

La documentation est transmise avec les livrables sur la plateforme OpenClassrooms.

## Auteur

**Rebecca Bajazet**


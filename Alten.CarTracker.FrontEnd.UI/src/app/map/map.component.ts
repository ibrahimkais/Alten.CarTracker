import { Component, OnInit } from '@angular/core';
import { loadCss, loadModules } from 'esri-loader';
import { DataService } from '../services/data.service';
import { Car } from '../models/Car';
import { ICarStatusChanged } from '../Interfaces/ICarStatusChanged';
import { CarStatus } from '../models/carStatus';
import { CarDisconnected } from '../models/carDisconnected';
import { SignalRService } from '../services/signal-rservice.service';
import { SidenavService } from '../services/SideNavService';
import { MapControlService } from '../services/map-control.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, ICarStatusChanged {

  webScene: any;
  webView: any;
  carsGraphiclayer: any;
  routesGraphiclayer: any;
  statusGraphicLayer: any;
  esriModules: any = {};

  carSymbol = {
    type: 'point-3d',
    symbolLayers: [{
      type: 'icon',
      resource: {
        href: 'assets/images/car2.png'
      },
      size: 80,
      outline: {
        color: 'white',
        size: 4
      }
    }],
    verticalOffset: {
      screenLength: 15,
      maxWorldLength: 300,
      minWorldLength: 50
    },
    callout: {
      type: 'line',
      color: 'white',
      size: 4,
      border: {
        color: '#D13470'
      }
    }
  };

  ESRI = [
    {
      'module': 'esri/WebScene',
      'key': 'WebScene'
    },
    {
      'module': 'esri/views/SceneView',
      'key': 'SceneView'
    },
    {
      'module': 'esri/layers/SceneLayer',
      'key': 'SceneLayer'
    },
    {
      'module': 'esri/layers/GraphicsLayer',
      'key': 'GraphicsLayer'
    },
    {
      'module': 'esri/Graphic',
      'key': 'Graphic'
    },
    {
      'module': 'esri/symbols/SimpleMarkerSymbol',
      'key': 'SimpleMarkerSymbol'
    },
    {
      'module': 'esri/Color',
      'key': 'Color'
    },
    {
      'module': 'esri/widgets/Home',
      'key': 'Home'
    },
    {
      'module': 'esri/widgets/Expand',
      'key': 'Expand'
    },
    {
      'module': 'esri/widgets/LayerList',
      'key': 'LayerList'
    },
  ];

  constructor(private dataService: DataService,
    private signalRService: SignalRService,
    private sidenav: SidenavService,
    private mapControlService: MapControlService) { }

  ngOnInit() {
    this.signalRService.subscribe(this);
    this.mapControlService.setDrawRoute(this.drawRoute);
    this.mapControlService.setLocatePoint(this.locate);
    this.mapControlService.setClear(this.clearRoutes);

    loadCss('https://js.arcgis.com/4.10/esri/css/main.css');
    loadModules(this.ESRI.map(m => m.module)).then(modules => {
      modules.forEach((module, index) => this.esriModules[this.ESRI[index].key] = module);

      const { WebScene, SceneView, SceneLayer, GraphicsLayer, Home, Expand, LayerList } = this.esriModules;

      const sceneLayer = new SceneLayer({
        portalItem: {
          id: 'f5c497819a374941b0ce8d9b0e979819'
        },
        title: 'San Francisco buildings',
        popupEnabled: false,
        visible: false,
        renderer: {
          type: 'simple',
          symbol: {
            type: 'mesh-3d',
            symbolLayers: [{
              type: 'fill',
              material: {
                color: '#ffffff',
                colorMixMode: 'replace'
              },
              edges: {
                type: 'solid',
                color: [0, 0, 0, 0.6],
                size: 1
              }
            }]
          }
        }
      });

      this.routesGraphiclayer = new GraphicsLayer({
        title: 'Routes',
      });

      this.statusGraphicLayer = new GraphicsLayer({
        title: 'Status Points',
        popupTemplate: {
          content: '<p>X: {x}</p><p>Y: {y}</p>',
        }
      });

      this.carsGraphiclayer = new GraphicsLayer({
        title: 'Cars',
        popupTemplate: {
          content: 'Vin Code: {vinCode}',
        }
      });

      this.webScene = new WebScene({
        basemap: 'streets-night-vector',
        layers: [this.carsGraphiclayer, this.statusGraphicLayer, this.routesGraphiclayer, sceneLayer]
      });

      this.webView = new SceneView({
        container: 'mapDiv',
        map: this.webScene,
        ui: {
          components: ['attribution', 'navigation-toggle', 'compass', 'zoom'],
        }
      });
      this.webView.ui.move(['navigation-toggle', 'compass', 'zoom'], 'top-right');

      const home = new Home({
        view: this.webView,
        goToOverride: () => this.zoomToDefault(),
      });

      home.on('go', () => this.zoomToDefault());
      this.webView.ui.add(home, 'top-right');

      const layerList = new LayerList({
        view: this.webView,
      });

      const layerListExpand = new Expand({
        expandIconClass: 'esri-icon-layer-list',
        expandTooltip: 'Expand LayerList',
        view: this.webView,
        content: layerList
      });

      this.webView.ui.add(layerListExpand, {
        position: 'top-right',
      });

      this.webScene.watch('loaded', (newValue: boolean, oldValue: boolean, propertyName: string, target: any) => {
        if (newValue === true) {
          setTimeout(() => {
            this.zoomToDefault();
            this.sidenav.open();
          }, 7000);
        }
      });
    }
    );
  }

  public carStatusChanged(carStatus: CarStatus) {
    this.updateCarLocation(carStatus.vinCode, carStatus.x, carStatus.y);
  }

  public carDisconnectedChanged(carDisconnected: CarDisconnected) { }

  zoomToDefault = () => this.webView.goTo({
    'position': {
      'spatialReference': {
        'wkid': 102100
      },
      'x': -13618785.025166072,
      'y': 4537012.796847662,
      'z': 8437.146268719807
    },
    'heading': 309.68298522946975,
    'tilt': 52.024161885490834
  })

  zoomToCar = (vinCode: string) => {
    const car = this.carsGraphiclayer.graphics.find(g => {
      return g.attributes.vinCode === vinCode;
    });
    if (car) {
      this.webView.goTo({
        'position': {
          latitude: (car.geometry.y - 0.00359893540083),
          longitude: (car.geometry.x + 0.00042102946339),
          'z': 164.40829646680504
        },
        'heading': 353.4351432456543,
        'tilt': 67.76378553470832
      }
      );
    }
  }

  updateCarLocation = (vinCode: string, x: number, y: number) => {
    if (this.carsGraphiclayer && this.carsGraphiclayer.graphics) {
      const car = this.carsGraphiclayer.graphics.find(g => {
        return g.attributes.vinCode === vinCode;
      });
      this.carsGraphiclayer.remove(car);

      const { Graphic } = this.esriModules;
      const newCar = new Graphic({
        geometry: {
          type: 'point',
          x: x,
          y: y
        },
        symbol: this.carSymbol,
        attributes: {
          vinCode: vinCode
        }
      });
      this.carsGraphiclayer.add(newCar);
    }
  }

  drawRoute = (car: Car) => {
    this.clearRoutes();
    const routData = this.dataService.getData(car.vin);
    const carGraphic = this.carsGraphiclayer.graphics.find(g => {
      return g.attributes.vinCode === car.vin;
    });

    if (carGraphic) {
      let index: number;
      const point = routData.data.gpx.trk.trkseg.trkpt.find((p, i) => {
        index = i;
        return Number(p._lat) === carGraphic.geometry.y && Number(p._lon) === carGraphic.geometry.x;
      });

      if (point) {
        const pointsRange = routData.data.gpx.trk.trkseg.trkpt.slice(0, index).map(p => [p._lon, p._lat]);
        const polyline = {
          type: 'polyline',
          paths: [pointsRange]
        };

        const { Graphic } = this.esriModules;

        const polylineSymbol = {
          type: 'simple-line',
          color: [226, 119, 40],
          width: 4
        };

        const polylineAtt = {
          vinCode: car.vin,
        };

        const polylineGraphic = new Graphic({
          geometry: polyline,
          symbol: polylineSymbol,
          attributes: polylineAtt
        });

        this.routesGraphiclayer.add(polylineGraphic);

        car.carStatuses.forEach(status => {
          this.locate(status);
        });
      }
    }
  }

  locate = (status: CarStatus) => {
    const { Graphic, SimpleMarkerSymbol, Color } = this.esriModules;

    const marker = new SimpleMarkerSymbol();
    marker.size = 24;
    marker.path = `M16,3.5c-4.142,0-7.5,3.358-7.5,7.5c0,4.143,7.5,18.121,7.5,
    18.121S23.5,15.143,23.5,11C23.5,6.858,20.143,3.5,16,3.5z M16,14.584c-1.979,
    0-3.584-1.604-3.584-3.584S14.021,7.416,16,7.416S19.584,9.021,19.584,11S17.979,14.584,16,14.584z`;
    marker.style = 'path';
    marker.color = new Color([56, 168, 0, 1]);

    const geom = {
      type: 'point',
      x: status.x,
      y: status.y
    };

    const pointAtt = {
      x: status.x,
      y: status.y
    };

    const pointG = new Graphic({
      geometry: geom,
      symbol: marker,
      attributes: pointAtt
    });

    this.statusGraphicLayer.add(pointG);
  }

  clearRoutes = () => {
    this.routesGraphiclayer.removeAll();
    this.statusGraphicLayer.removeAll();
  }
}

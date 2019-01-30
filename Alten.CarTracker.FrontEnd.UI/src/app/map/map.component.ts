import { Component, OnInit } from '@angular/core';
import { loadCss, loadModules } from 'esri-loader';
import { DataService } from '../services/data.service';
import { Car } from '../models/Car';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

  webScene: any;
  webView: any;
  carsGraphiclayer: any;
  routesGraphiclayer: any;
  statusGraphicLayer: any;
  esriModules: any[] = [];

  carSymbol = {
    type: 'point-3d',
    symbolLayers: [{
      type: 'icon',
      resource: {
        href: 'assets/images/logo.png'
      },
      size: 20,
      outline: {
        color: 'white',
        size: 2
      }
    }],
    verticalOffset: {
      screenLength: 40,
      maxWorldLength: 200,
      minWorldLength: 35
    },
    callout: {
      type: 'line',
      color: 'white',
      size: 2,
      border: {
        color: '#D13470'
      }
    }
  };

  constructor(private dataService: DataService) { }

  ngOnInit() {
    loadCss('https://js.arcgis.com/4.10/esri/css/main.css');
    loadModules([
      'esri/WebScene',
      'esri/views/SceneView',
      'esri/layers/SceneLayer',
      'esri/layers/GraphicsLayer',
      'esri/Graphic',
      'esri/symbols/SimpleMarkerSymbol',
      'esri/Color',
      'esri/widgets/Home',
      'esri/widgets/Expand',
      'esri/widgets/LayerList',
    ]).then(modules => {
      this.esriModules = modules;
      const [WebScene, SceneView, SceneLayer, GraphicsLayer, , , , Home, Expand, LayerList] = modules;

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
        title: 'Status Points'
      });

      this.carsGraphiclayer = new GraphicsLayer({
        title: 'Cars',
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
        goToOverride: () => this.zoomToDefault().bind(this),
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
        if (newValue === true) { setTimeout(() => this.zoomToDefault(), 5000); }
      });
    }
    );
  }

  zoomToDefault = () =>
    this.webView.goTo(
      {
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
      }
    )

  zoomToCar = (carId: string) => {
    const car = this.carsGraphiclayer.graphics.find(g => {
      return g.attributes.carId === carId;
    });
    this.carsGraphiclayer.view.goTo({
      'position': {
        'spatialReference': {
          'wkid': 102100
        },
        'x': car.geometry.x,
        'y': car.geometry.y,
        'z': car.geometry.z
      },
      'heading': 351.28636026422873,
      'tilt': 78.1896252071027
    }
    );
  }

  updateCarLocation = (carId: string, x: number, y: number) => {
    const car = this.carsGraphiclayer.graphics.find(g => {
      return g.attributes.carId === carId;
    });
    this.carsGraphiclayer.remove(car);

    const [Graphic] = this.esriModules;
    const newCar = new Graphic({
      geometry: {
        type: 'point',
        x: x,
        y: y
      },
      symbol: this.carSymbol,
      attributes: {
        pkCarId: carId
      }
    });
    this.carsGraphiclayer.add(newCar);
  }

  drawRoute = (car: Car) => {
    const data = this.dataService.getData(car.vin);
    const carGraphic = this.carsGraphiclayer.graphics.find(g => {
      return g.attributes.carId === car.vin;
    });

    let index: number;
    const point = data.gpx.trk.trkpt.find((p, i) => {
      index = i;
      return p._lat === carGraphic.geometry.y && p._lon === carGraphic.geometry.y;
    });
    if (point) {
      const pointsRange = data.gpx.trk.trkpt.slice(0, index).map(p => [p._lon, p._lat]);
      const polyline = {
        type: 'polyline',
        paths: [pointsRange]
      };

      this.clearRoutes();
      const [Graphic, SimpleMarkerSymbol, Color] = this.esriModules;

      const polylineSymbol = {
        type: 'simple-line',
        color: [226, 119, 40],
        width: 4
      };

      const polylineAtt = {
        carId: car.vin,
      };

      const polylineGraphic = new Graphic({
        geometry: polyline,
        symbol: polylineSymbol,
        attributes: polylineAtt
      });

      this.routesGraphiclayer.add(polylineGraphic);

      const marker = new SimpleMarkerSymbol();
      marker.setSize(24);
      marker.setPath(`M16,3.5c-4.142,0-7.5,3.358-7.5,7.5c0,4.143,7.5,18.121,7.5,
      18.121S23.5,15.143,23.5,11C23.5,6.858,20.143,3.5,16,3.5z M16,14.584c-1.979,
      0-3.584-1.604-3.584-3.584S14.021,7.416,16,7.416S19.584,9.021,19.584,11S17.979,14.584,16,14.584z`);
      marker.setStyle(SimpleMarkerSymbol.STYLE_PATH);
      marker.setColor(new Color([56, 168, 0, 1]));

      car.carStatuses.forEach(status => {
        const geom = {
          type: 'point',
          x: status.x,
          y: status.y
        };
        const pointG = new Graphic({
          geometry: geom,
          symbol: marker
        });
        this.statusGraphicLayer.add(pointG);
      });
    }
  }

  clearRoutes = () => {
    this.routesGraphiclayer.clear();
    this.statusGraphicLayer.clear();
  }
}

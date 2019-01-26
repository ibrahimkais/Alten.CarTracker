import { Component, OnInit } from '@angular/core';
import { loadCss, loadModules } from 'esri-loader';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    loadCss('https://js.arcgis.com/4.10/esri/css/main.css');
    loadModules(['esri/WebScene', 'esri/views/SceneView', 'esri/layers/SceneLayer', 'esri/layers/GraphicsLayer']).then(modules => {
      const [WebScene, SceneView, SceneLayer, GraphicsLayer] = modules;
      const renderer = {
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
      };

      const sceneLayer = new SceneLayer({
        portalItem: {
          id: 'f5c497819a374941b0ce8d9b0e979819'
        },
        title: 'San Francisco buildings',
        renderer: renderer
      });

      const webscene = new WebScene({
       basemap: 'streets-night-vector',
        layers: [sceneLayer]
      });

      const view = new SceneView({
        container: 'mapDiv',
        map: webscene,
      });

      webscene.watch('loaded', (newValue, oldValue, propertyName, target ) => {
        if (newValue === true) {
          setTimeout(() => {
            view.goTo(
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
            );
          }, 5000);
        }
      });
    });
  }
}

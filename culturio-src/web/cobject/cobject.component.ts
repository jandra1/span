import { map, Observable, shareReplay } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { CobjectDto } from './models/cobjectDto';
import { CobjectService } from './services/cobject.service';
import * as mapboxgl from 'mapbox-gl';
import { GetCobjectsDto } from './models/getCobjectsDto';

@Component({
  selector: 'app-cobject',
  templateUrl: './cobject.component.html',
  styleUrls: ['./cobject.component.scss'],
})
export class CobjectComponent implements OnInit {
  cultureObjects$: Observable<CobjectDto[]>;
  cobjectById$: Observable<CobjectDto>;
  totalCount$: Observable<number>;

  cultureObjects: CobjectDto[];
  totalCount: number;

  searchValue = '';
  visible = false;
  sortOrder: string;

  map: mapboxgl.Map;
  style = 'mapbox://styles/mapbox/streets-v11';
  lat = 45.8;
  lng = 15.98;

  constructor(private cobjectService: CobjectService) {}

  ngOnInit(): void {
    this.getcobjects();

    this.map = new mapboxgl.Map({
      accessToken:
        'pk.eyJ1IjoiamFrb3ZpbmhvIiwiYSI6ImNsN3B6c2NvZDAwbmMzdnNhNjFzb3k0OWMifQ.PLpSSPMdnWYBccaXNjgb7A',
      container: 'map',
      style: this.style,
      zoom: 12.5,
      center: [this.lng, this.lat],
    });

    //const marker = new mapboxgl.Marker()
    //.setLngLat([15.98, 45.80])
    //.addTo(this.map);

    // Add map controls
    this.map.addControl(new mapboxgl.NavigationControl());
  }

  getcobjects() {
    const model: GetCobjectsDto = {
      sortOrder: 'asc',
      searchValue: '',
      page: 1,
      pageSize: 10,
    };
    const source = this.cobjectService
      .getCobjects(model)
      .subscribe((result) => {
        this.cultureObjects = result.cultureObjects;
        this.totalCount = result.totalCount;

        this.cultureObjects.forEach((cultureObject) => {
          const el = document.createElement('div');
          const width = 50;
          const height = 50;
          el.className = 'marker';
          switch (cultureObject.cultureObjectType) {
            case 'Cinema': {
              el.style.backgroundImage = "url('/assets/img/cinema.png')";
              break;
            }
            case 'Museum': {
              el.style.backgroundImage = "url('/assets/img/museum.png')";
              break;
            }
            case 'Theater': {
              el.style.backgroundImage = "url('/assets/img/theater.png')";
              break;
            }
          }
          el.style.width = `${width}px`;
          el.style.height = `${height}px`;
          el.style.backgroundSize = '100%';

          el.addEventListener('click', () => {
            window.alert(cultureObject.name);
          });

          new mapboxgl.Marker(el)
            .setLngLat([cultureObject.longitude, cultureObject.latitude])
            .addTo(this.map);
        });
      });
  }

  getcobjectById(id: number) {
    this.cobjectById$ = this.cobjectService.getCobjectById(id);
  }

  search() {}
}


import { RouteReuseStrategy, ActivatedRouteSnapshot, DetachedRouteHandle } from '@angular/router';

export class SimpleReuseStrategy implements RouteReuseStrategy {
    static cacheRouters = new Map<string, DetachedRouteHandle>();

    public static deleteRouteCache(url): void {
        if (SimpleReuseStrategy.cacheRouters.has(url)) {
            const handle: any = SimpleReuseStrategy.cacheRouters.get(url);
            try {
                handle.componentRef.destory();
            } catch (e) {
                console.warn('组件已被销毁',e);
             }
            SimpleReuseStrategy.cacheRouters.delete(url);
        }
    }

    public static deleteAllRouteCache(): void {
        SimpleReuseStrategy.cacheRouters.forEach((handle: any, key) => {
            SimpleReuseStrategy.deleteRouteCache(key);
        });
    }

    // one 进入路由触发，是否同一路由时复用路由
    shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot,): boolean {
        return future.routeConfig === curr.routeConfig &&
        JSON.stringify(future.params) === JSON.stringify(curr.params);
    }

    // 获取存储路由
    retrieve(route: ActivatedRouteSnapshot): DetachedRouteHandle {
        const url = this.getFullRouteURL(route);
        if (route.data.keep && SimpleReuseStrategy.cacheRouters.has( url) ) {
            return SimpleReuseStrategy.cacheRouters.get(url);
        } else {
            return null;
        }
    }

    // 是否允许复用路由
    shouldDetach(route: ActivatedRouteSnapshot): boolean {
        return Boolean(route.data.keep);
    }
    // 当路由离开时会触发，存储路由
    store(route: ActivatedRouteSnapshot, handle: DetachedRouteHandle): void {
        const url = this.getFullRouteURL(route);
       // 先把之前缓存的删除,
        SimpleReuseStrategy.cacheRouters.forEach((handle: any, key) => {
            SimpleReuseStrategy.deleteRouteCache(key);
        });
        SimpleReuseStrategy.cacheRouters.set(url, handle);
    }
    //  是否允许还原路由
    shouldAttach(route: ActivatedRouteSnapshot): boolean {
        const url = this.getFullRouteURL(route);
        return Boolean(route.data.keep) && SimpleReuseStrategy.cacheRouters.has(url);
    }

    // 获取当前路由url
    private getFullRouteURL(route: ActivatedRouteSnapshot): string {
        const { pathFromRoot } = route;
        let fullRouteUrlPath: string[] = [];
        pathFromRoot.forEach((item: ActivatedRouteSnapshot) => {
            fullRouteUrlPath = fullRouteUrlPath.concat( this.getRouteUrlPath(item) );
        });
        return `/${fullRouteUrlPath.join('/')}`;

    }
    private getRouteUrlPath(route: ActivatedRouteSnapshot) {
        return route.url.map(urlSegment => urlSegment.path);
    }

    
}
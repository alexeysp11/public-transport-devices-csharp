create table if not exists public.pt_device
(
    pt_device_id serial primary key, 
    device_uid text not null, 
    device_type_id integer
);

create unique index pt_device_device_uid_idx ON public.pt_device (device_uid);

create table if not exists public.pt_device_info
(
    pt_device_info_id serial primary key, 
    pt_device_id integer references public.pt_device (pt_device_id), 
    latitude float8,
    longitude float8
);
